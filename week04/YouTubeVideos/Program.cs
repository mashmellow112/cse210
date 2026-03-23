using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create first video
        Video video1 = new Video("C# Programming Tutorial for Beginners", "CodeMaster", 1200);
        video1.AddComment(new Comment("Alice", "Great tutorial! Very clear explanations."));
        video1.AddComment(new Comment("Bob", "This helped me understand classes finally!"));
        video1.AddComment(new Comment("Charlie", "Would love to see more content like this."));
        video1.AddComment(new Comment("Diana", "Excellent work! Keep it up."));
        videos.Add(video1);

        // Create second video
        Video video2 = new Video("Understanding Abstraction in OOP", "TechTeacher", 900);
        video2.AddComment(new Comment("Eve", "Abstraction finally makes sense now."));
        video2.AddComment(new Comment("Frank", "Very well explained with good examples."));
        video2.AddComment(new Comment("Grace", "Best video on this topic I've found."));
        videos.Add(video2);

        // Create third video
        Video video3 = new Video("Building Your First C# Project", "DevJourney", 1800);
        video3.AddComment(new Comment("Henry", "Step by step guide was perfect."));
        video3.AddComment(new Comment("Ivy", "I built my first app thanks to this!"));
        video3.AddComment(new Comment("Jack", "Clear and concise. Thank you!"));
        videos.Add(video3);

        // Create fourth video
        Video video4 = new Video("Object-Oriented Programming Fundamentals", "LearnWithPro", 2400);
        video4.AddComment(new Comment("Kate", "Comprehensive explanation of OOP concepts."));
        video4.AddComment(new Comment("Leo", "Perfect for beginners."));
        video4.AddComment(new Comment("Mia", "The examples really helped me understand."));
        videos.Add(video4);

        // Iterate through videos and display information
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            List<Comment> comments = video.GetComments();
            foreach (Comment comment in comments)
            {
                Console.WriteLine($"  - {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}