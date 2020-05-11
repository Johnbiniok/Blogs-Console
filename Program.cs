using System;
using System.Linq;
using BlogsConsole.Models;
using NLog;

namespace BlogsConsole
{
    class MainClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            logger.Info("Program started");
            optionsForUser();
        }

        private static void optionsForUser()
        {
            string userChoice = "";
            //loop for menu and user options
            do
            {
                Console.WriteLine("Please Choose an option");
                Console.WriteLine("1. Display All Blogs");
                Console.WriteLine("2. Add Blog");
                Console.WriteLine("3. Create Post");
                Console.WriteLine("4. Exit Program");
                userChoice = Console.ReadLine();

                //use a switch statement to guide actions based on user choice
                switch (userChoice)
                {
                    //display all blogs
                    case "1":
                        readBlogsFromDatabase();
                        break;
                    //add a blog to the database
                    case "2":
                        addBlogToDatabase();
                        break;
                    //create a post in a blog
                    case "3":
                        chooseBlogChoice();
                        //addPosttoBlog(blogChoice);
                        break;
                    //exits the program
                    case "4":
                        Console.WriteLine("Goodbye!");
                        break;
                }
            } while (userChoice != "4");

            logger.Info("Program ended");
        }

        //this method is used to read all blogs and return the blog chosen by the user
        public static void chooseBlogChoice()
        {
            bool found = false;
            do
            {
                try
                {
                    found = false;
                    Console.WriteLine("Which blog would you like to write to?");
                    readBlogsFromDatabase();
                    string blogChoice = Console.ReadLine();

                    //make sure the chosen blog exists, otherwise make them choose another
                    var db = new BloggingContext();
                    var query = db.Blogs.OrderBy(b => b.Name);
                    Post newUserPost = new Post();
                    foreach (var item in query)
                    {
                        if (item.Name.Equals(blogChoice))
                        {
                            found = true;
                            string postTitle = "";
                            string postContent = "";
                            Console.WriteLine("Title: ");
                            postTitle = Console.ReadLine();
                            Console.WriteLine("Content: ");
                            postContent = Console.ReadLine();
                            newUserPost = new Post {Title = postTitle, Content = postContent, BlogId = item.BlogId, Blog = item};

                            //db.Posts.Add(new Post {Title = postTitle, Content = postContent});
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("That Blog was not found");
                    }
                    else
                    {
                        db.AddPost(newUserPost);
                        db.SaveChanges();
                    }
                    /*else
                    {
                        string postTitle = "";
                        string postContent = "";
                        Console.WriteLine("Title: ");
                        postTitle = Console.ReadLine();
                        Console.WriteLine("Content: ");
                        postContent = Console.ReadLine();
                        Post newUserPost = new Post {Title = postTitle, Content = postContent};
                        db.AddPost(newUserPost);
                        db.SaveChanges();
                    }*/
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            } while (!found);

        }

        public static void addPosttoBlog(Blog blogName)
        {
            string postTitle = "";
            string postContent = "";
            Console.WriteLine("Title: ");
            postTitle = Console.ReadLine();
            Console.WriteLine("Content: ");
            postContent = Console.ReadLine();
            //db.AddPost.Add(new Post {Title = postTitle, Content = postContent});
        }



        /* string postTitle = "";
         string postContent = "";
         bool found = false;
         try
         {
             var db = new BloggingContext();
             var currentBlog = "";
             var query = db.Blogs.OrderBy(b => b.Name);

             Console.WriteLine("All blogs in the database:");
             foreach (var item in query)
             {
                 if (item.Name.Equals(blogName))
                 {
                     found = true;
                     Console.WriteLine("Title: ");
                     postTitle = Console.ReadLine();
                     Console.WriteLine("Content: ");
                     postContent = Console.ReadLine();
                 }
             }

             if (!found)
             {
                 Console.WriteLine("");
             }

         }
         catch (Exception ex)
         {
             logger.Error(ex.Message);
         }
     }*/

     //This method is to add a blog to the database
     public static void addBlogToDatabase()
     {
         try
         {
             // Create and save a new Blog
             Console.Write("Enter a name for a new Blog: ");
             var name = Console.ReadLine();

             var blog = new Blog {Name = name};

             var db = new BloggingContext();
             db.AddBlog(blog);
             logger.Info("Blog added - {name}", name);
         }
         catch (Exception ex)
         {
             logger.Error(ex.Message);
         }
     }

            //this method is to read all the blogs from the database
            private static void readBlogsFromDatabase()
            {
                try
                {
                    var db = new BloggingContext();
                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
    }


