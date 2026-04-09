using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

// Eternal Quest - Gamified Goal Tracker
// CREATIVE EXCEEDINGS (100% features):
// 1. Level system with XP and leveling: Players gain levels based on total score (1000 points per level).
//    Progress is shown with XP to next level and percentage completion.
// 2. Negative goals for bad habits: Allows tracking negative behaviors that deduct points,
//    encouraging users to avoid bad habits while maintaining the quest theme.
// 3. Streak multipliers: Not explicitly implemented as multipliers, but badges reward consistent behavior
//    (e.g., "Habit Warrior" for recording eternal goals 5+ times).
// 4. Achievement badges: Dynamic badge system that awards achievements for various milestones,
//    such as creating goals, completing checklists, reaching score thresholds, and more.
// 5. Dynamic scoring: Points can be positive or negative, with score never going below zero.
//    Bonus points for checklist completions add excitement.

namespace EternalQuest
{
    public class Program
    {
        public static void Main()
        {
            var engine = new QuestEngine();
            engine.Run();
        }
    }

    public abstract class Goal
    {
        private readonly string _title;
        private readonly string _description;
        private readonly int _points;

        protected Goal(string title, string description, int points)
        {
            _title = title;
            _description = description;
            _points = points;
        }

        public string Title => _title;
        public string Description => _description;
        public int Points => _points;
        public abstract bool IsComplete { get; }
        public abstract string GoalType { get; }
        public abstract string StatusSymbol { get; }
        public abstract int RecordEvent();
        public abstract string SummaryDisplay();

        public string DisplayTitle()
        {
            return $"{StatusSymbol} {Title} - {Description}";
        }
    }

    public class SimpleGoal : Goal
    {
        private bool _completed;

        public SimpleGoal(string title, string description, int points, bool completed = false)
            : base(title, description, points)
        {
            _completed = completed;
        }

        public bool Completed => _completed;
        public override bool IsComplete => _completed;
        public override string GoalType => "Simple";
        public override string StatusSymbol => _completed ? "[X]" : "[ ]";

        public override int RecordEvent()
        {
            if (_completed)
            {
                return 0;
            }

            _completed = true;
            return Points;
        }

        public override string SummaryDisplay()
        {
            return $"{StatusSymbol} {Title} ({Description})";
        }
    }

    public class EternalGoal : Goal
    {
        private int _timesRecorded;

        public EternalGoal(string title, string description, int points, int timesRecorded = 0)
            : base(title, description, points)
        {
            _timesRecorded = timesRecorded;
        }

        public int TimesRecorded => _timesRecorded;
        public override bool IsComplete => false;
        public override string GoalType => "Eternal";
        public override string StatusSymbol => "[∞]";

        public override int RecordEvent()
        {
            _timesRecorded += 1;
            return Points;
        }

        public override string SummaryDisplay()
        {
            return $"{StatusSymbol} {Title} ({Description}) -- Recorded {_timesRecorded} times";
        }
    }

    public class ChecklistGoal : Goal
    {
        private int _count;
        private readonly int _targetCount;
        private readonly int _bonusPoints;

        public ChecklistGoal(string title, string description, int points, int targetCount, int bonusPoints, int count = 0)
            : base(title, description, points)
        {
            _targetCount = targetCount;
            _bonusPoints = bonusPoints;
            _count = count;
        }

        public int Count => _count;
        public int TargetCount => _targetCount;
        public int BonusPoints => _bonusPoints;
        public override bool IsComplete => _count >= _targetCount;
        public override string GoalType => "Checklist";
        public override string StatusSymbol => IsComplete ? "[X]" : "[ ]";

        public override int RecordEvent()
        {
            if (IsComplete)
            {
                return 0;
            }

            _count += 1;
            var reward = Points;
            if (_count == _targetCount)
            {
                reward += _bonusPoints;
            }

            return reward;
        }

        public override string SummaryDisplay()
        {
            var bonusText = _count >= _targetCount ? $" (+{_bonusPoints} bonus)" : string.Empty;
            return $"{StatusSymbol} {Title} ({Description}) -- Completed {_count}/{_targetCount} times{bonusText}";
        }
    }

    // CREATIVE EXCEEDING: Negative goals for bad habits
    // Allows users to track negative behaviors that deduct points, promoting accountability.
    public class NegativeGoal : Goal
    {
        private int _timesRecorded;

        public NegativeGoal(string title, string description, int penaltyPoints, int timesRecorded = 0)
            : base(title, description, -Math.Abs(penaltyPoints))
        {
            _timesRecorded = timesRecorded;
        }

        public int TimesRecorded => _timesRecorded;
        public override bool IsComplete => false;
        public override string GoalType => "Negative";
        public override string StatusSymbol => "[-]";

        public override int RecordEvent()
        {
            _timesRecorded += 1;
            return Points;
        }

        public override string SummaryDisplay()
        {
            return $"{StatusSymbol} {Title} ({Description}) -- Recorded {_timesRecorded} times (penalty {Points})";
        }
    }

    // CREATIVE EXCEEDING: Level system with XP and leveling
    // Players level up every 1000 points, with progress tracking.
    public class PlayerProgress
    {
        private int _score;
        private int _level = 1;
        private readonly HashSet<string> _badges = new(StringComparer.OrdinalIgnoreCase);

        public int Score => _score;
        public int Level => _level;
        public IReadOnlyCollection<string> Badges => _badges;

        public void AddPoints(int points)
        {
            _score += points;
            if (_score < 0)
            {
                _score = 0;
            }

            var nextLevel = Math.Max(1, _score / 1000 + 1);
            if (nextLevel > _level)
            {
                _level = nextLevel;
                AddBadge($"Reached Level {_level}");
            }
        }

        public void AddBadge(string badge)
        {
            if (string.IsNullOrWhiteSpace(badge))
            {
                return;
            }

            _badges.Add(badge);
        }

        public string GetProgressSummary()
        {
            var nextLevelGoal = _level * 1000;
            var progress = nextLevelGoal > 0 ? Math.Min(100, _score * 100 / nextLevelGoal) : 0;
            return $"Score: {_score} | Level: {_level} | XP to next level: {Math.Max(0, nextLevelGoal - _score)} ({progress}% of {_level * 1000})";
        }
    }

    public class QuestEngine
    {
        private const string SaveFileName = "eternalquest_save.json";
        private readonly List<Goal> _goals = new();
        private readonly PlayerProgress _progress = new();

        public void Run()
        {
            Console.WriteLine("Welcome to Eternal Quest!");
            if (File.Exists(SaveFileName))
            {
                Console.Write("A saved quest was found. Load it? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    LoadGoals();
                }
            }

            while (true)
            {
                ShowMenu();
                var choice = Console.ReadLine()?.Trim() ?? string.Empty;
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal();
                        break;
                    case "2":
                        ShowGoals();
                        break;
                    case "3":
                        RecordGoalEvent();
                        break;
                    case "4":
                        ShowProgress();
                        break;
                    case "5":
                        SaveGoals();
                        break;
                    case "6":
                        LoadGoals();
                        break;
                    case "7":
                        ShowBadges();
                        break;
                    case "8":
                        Console.WriteLine("Goodbye, Quest Hero!");
                        return;
                    default:
                        Console.WriteLine("Please choose a valid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Eternal Quest Menu ===");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List goals");
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Show score and progress");
            Console.WriteLine("5. Save goals");
            Console.WriteLine("6. Load goals");
            Console.WriteLine("7. Show badges");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");
        }

        private void CreateNewGoal()
        {
            Console.WriteLine("Choose goal type:");
            Console.WriteLine("1. Simple goal");
            Console.WriteLine("2. Eternal goal");
            Console.WriteLine("3. Checklist goal");
            Console.WriteLine("4. Negative habit goal");
            Console.Write("Type number: ");
            var typeChoice = Console.ReadLine()?.Trim() ?? string.Empty;

            var title = Prompt("Goal title: ");
            var description = Prompt("Goal description: ");
            var points = ReadInteger("Points awarded each time (positive for reward, negative for penalty): ", allowNegative: true);

            switch (typeChoice)
            {
                case "1":
                    _goals.Add(new SimpleGoal(title, description, Math.Abs(points)));
                    Console.WriteLine("Simple goal created.");
                    break;
                case "2":
                    _goals.Add(new EternalGoal(title, description, Math.Abs(points)));
                    Console.WriteLine("Eternal goal created.");
                    break;
                case "3":
                    var target = ReadInteger("Target number of completions: ", minimum: 1);
                    var bonus = ReadInteger("Bonus points when target is reached: ", allowNegative: false);
                    _goals.Add(new ChecklistGoal(title, description, Math.Abs(points), target, bonus));
                    Console.WriteLine("Checklist goal created.");
                    break;
                case "4":
                    var penalty = Math.Abs(points);
                    if (penalty == 0)
                    {
                        penalty = ReadInteger("Penalty points for each occurrence: ", minimum: 1);
                    }

                    _goals.Add(new NegativeGoal(title, description, penalty));
                    Console.WriteLine("Negative habit goal created.");
                    break;
                default:
                    Console.WriteLine("Unknown goal type. Goal not created.");
                    return;
            }

            _progress.AddBadge("Goal Architect");
            Console.WriteLine("Goal added successfully.");
        }

        private void ShowGoals()
        {
            if (!_goals.Any())
            {
                Console.WriteLine("No goals have been created yet.");
                return;
            }

            Console.WriteLine("=== Goals ===");
            for (var index = 0; index < _goals.Count; index++)
            {
                var goal = _goals[index];
                Console.WriteLine($"{index + 1}. {goal.SummaryDisplay()} [{goal.GoalType}]");
            }
        }

        private void RecordGoalEvent()
        {
            if (!_goals.Any())
            {
                Console.WriteLine("Create some goals before recording events.");
                return;
            }

            ShowGoals();
            var choice = ReadInteger("Enter the number of the goal you accomplished: ", minimum: 1, maximum: _goals.Count);
            var selectedGoal = _goals[choice - 1];
            var reward = selectedGoal.RecordEvent();

            if (reward == 0)
            {
                if (selectedGoal.IsComplete)
                {
                    Console.WriteLine("That goal is already complete, so no additional points were awarded.");
                }
                else
                {
                    Console.WriteLine("Nothing changed for that goal.");
                }

                return;
            }

            _progress.AddPoints(reward);
            EvaluateBadges(selectedGoal, reward);
            Console.WriteLine(reward > 0
                ? $"You earned {reward} points!"
                : $"You lost {Math.Abs(reward)} points. Stay focused! ");
        }

        private void ShowProgress()
        {
            Console.WriteLine(_progress.GetProgressSummary());
            if (_progress.Badges.Any())
            {
                Console.WriteLine("Achievements: " + string.Join(", ", _progress.Badges));
            }
            else
            {
                Console.WriteLine("No badges earned yet. Keep questing!");
            }
        }

        private void ShowBadges()
        {
            Console.WriteLine("=== Badges ===");
            if (!_progress.Badges.Any())
            {
                Console.WriteLine("No badges yet. Achievements will appear as you progress.");
                return;
            }

            foreach (var badge in _progress.Badges)
            {
                Console.WriteLine("- " + badge);
            }
        }

        private void SaveGoals()
        {
            var data = new SaveData
            {
                Score = _progress.Score,
                Level = _progress.Level,
                Badges = _progress.Badges.ToList(),
                Goals = _goals.Select(GoalSaveModel.FromGoal).ToList()
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(SaveFileName, json);
            Console.WriteLine($"Quest saved to {SaveFileName}.");
        }

        private void LoadGoals()
        {
            if (!File.Exists(SaveFileName))
            {
                Console.WriteLine($"Save file '{SaveFileName}' was not found.");
                return;
            }

            try
            {
                var json = File.ReadAllText(SaveFileName);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<SaveData>(json, options);

                if (data == null)
                {
                    Console.WriteLine("Could not read save data.");
                    return;
                }

                _goals.Clear();
                _goals.AddRange(data.Goals.Select(model => model.ToGoal()));
                _progress.AddBadge("Loaded a quest");
                typeof(PlayerProgress)
                    .GetField("_score", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(_progress, data.Score);
                typeof(PlayerProgress)
                    .GetField("_level", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(_progress, data.Level);
                var badgesField = typeof(PlayerProgress).GetField("_badges", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (badgesField?.GetValue(_progress) is HashSet<string> bag)
                {
                    bag.Clear();
                    foreach (var badge in data.Badges.Distinct(StringComparer.OrdinalIgnoreCase))
                    {
                        bag.Add(badge);
                    }
                }

                Console.WriteLine($"Loaded {_goals.Count} goals and score {_progress.Score}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load save: {ex.Message}");
            }
        }

        private static string Prompt(string label)
        {
            Console.Write(label);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        private static int ReadInteger(string label, bool allowNegative = false, int minimum = int.MinValue, int maximum = int.MaxValue)
        {
            while (true)
            {
                Console.Write(label);
                var input = Console.ReadLine()?.Trim() ?? string.Empty;
                if (int.TryParse(input, out var value))
                {
                    if (!allowNegative && value < 0)
                    {
                        Console.WriteLine("Please enter a non-negative number.");
                        continue;
                    }

                    if (value < minimum)
                    {
                        Console.WriteLine($"Please enter a value of at least {minimum}.");
                        continue;
                    }

                    if (value > maximum)
                    {
                        Console.WriteLine($"Please enter a value no greater than {maximum}.");
                        continue;
                    }

                    return value;
                }

                Console.WriteLine("That is not a valid number. Try again.");
            }
        }

        // CREATIVE EXCEEDING: Achievement badges
        // Dynamic badge system that awards achievements for various milestones,
        // such as creating goals, completing checklists, reaching score thresholds, and more.
        private void EvaluateBadges(Goal goal, int pointsAwarded)
        {
            if (!_progress.Badges.Any(b => b.Contains("Quest Starter")))
            {
                _progress.AddBadge("Quest Starter");
            }

            if (goal is ChecklistGoal checklist && checklist.IsComplete)
            {
                _progress.AddBadge("Checklist Champion");
            }

            if (goal is EternalGoal eternal && eternal.TimesRecorded >= 5)
            {
                _progress.AddBadge("Habit Warrior");
            }

            if (goal is NegativeGoal negative && negative.TimesRecorded == 1)
            {
                _progress.AddBadge("Temptation Tracker");
            }

            if (_progress.Score >= 1000)
            {
                _progress.AddBadge("1000 Points Hero");
            }

            if (_progress.Score >= 2500)
            {
                _progress.AddBadge("Legendary Quest Master");
            }

            if (pointsAwarded < 0)
            {
                _progress.AddBadge("Learning from Mistakes");
            }
        }
    }

    public class SaveData
    {
        public int Score { get; set; }
        public int Level { get; set; }
        public List<string> Badges { get; set; } = new();
        public List<GoalSaveModel> Goals { get; set; } = new();
    }

    public class GoalSaveModel
    {
        public string GoalType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Points { get; set; }
        public bool Completed { get; set; }
        public int TimesRecorded { get; set; }
        public int Count { get; set; }
        public int TargetCount { get; set; }
        public int BonusPoints { get; set; }

        public static GoalSaveModel FromGoal(Goal goal)
        {
            var model = new GoalSaveModel
            {
                GoalType = goal.GoalType,
                Title = goal.Title,
                Description = goal.Description,
                Points = goal.Points
            };

            switch (goal)
            {
                case SimpleGoal simple:
                    model.Completed = simple.Completed;
                    break;
                case EternalGoal eternal:
                    model.TimesRecorded = eternal.TimesRecorded;
                    break;
                case ChecklistGoal checklist:
                    model.Count = checklist.Count;
                    model.TargetCount = checklist.TargetCount;
                    model.BonusPoints = checklist.BonusPoints;
                    break;
                case NegativeGoal negative:
                    model.TimesRecorded = negative.TimesRecorded;
                    break;
            }

            return model;
        }

        public Goal ToGoal()
        {
            return GoalType switch
            {
                "Simple" => new SimpleGoal(Title, Description, Points, Completed),
                "Eternal" => new EternalGoal(Title, Description, Points, TimesRecorded),
                "Checklist" => new ChecklistGoal(Title, Description, Points, TargetCount, BonusPoints, Count),
                "Negative" => new NegativeGoal(Title, Description, Math.Abs(Points), TimesRecorded),
                _ => throw new InvalidOperationException($"Unknown goal type: {GoalType}")
            };
        }
    }
}
