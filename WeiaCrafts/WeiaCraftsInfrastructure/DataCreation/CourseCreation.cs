using WeiaCraftsDomain.Entities;
using WeiaCraftsDomain.Enum;
using WeiaCraftsDomain.ValueObjects;

namespace WeiaCraftsInfrastructure.DataCreation;

public static class CourseCreation
{
    public static List<Course> GetPreconfiguredCourses()
    {
        return new List<Course>
        {
            new Course
            {
                Title = "Natural Inspired Tableware",
                Description = "Learn how to create natural-inspired tableware, including wheel throwing and hand-building techniques.",
                Level = CourseLevel.Beginner,
                AccessType = CourseAccessType.Paid,
                Status = CourseStatus.Ongoing,
                Language = CourseLanguage.English,
                IsKidFriendly = false,
                Price = new AmountWithCurrency { Amount = 29.99m, Currency = "USD" },
                Rating = 4.7,
                RatingCount = 150,
                CoverImageURL = "/images/courses/natural-tableware-cover.jpeg",
                PromoVideoURL = "https://www.youtube.com/embed/_6a7vOp7Nm0",
                WideRangeCategory = WideRangeCategory.ClayAndCeramics,
                DateOfCreation = DateTime.UtcNow.AddMonths(-5),
                LastUpdatedTimestamp = DateTime.UtcNow.AddMonths(-1),
                Lessons = new List<Lesson>
                {
                    new Lesson 
                    { 
                        Title = "Creating Your First Ceramic Mug", 
                        SequenceOrder = 1, 
                        ContentType = LessonContentType.Video, 
                        ContentData = "https://www.youtube.com/embed/JzE3g9KrdOQ", 
                        Description = "Learn the basics of hand-building a ceramic mug without a wheel." 
                    },
                    new Lesson 
                    { 
                        Title = "Introduction to Wheel Throwing", 
                        SequenceOrder = 2, 
                        ContentType = LessonContentType.Video, 
                        ContentData = "https://www.youtube.com/embed/Tllu3Qae9g0", 
                        Description = "Learn the fundamentals of using a pottery wheel." 
                    }
                }
            },
            new Course
            {
                Title = "Beginner's Knitting Journey",
                Description = "Master essential knitting patterns and techniques.",
                Level = CourseLevel.Beginner,
                AccessType = CourseAccessType.Free,
                Status = CourseStatus.Ongoing,
                Language = CourseLanguage.Arabic,
                IsKidFriendly = true,
                Price = new AmountWithCurrency { Amount = 0m, Currency = "USD" },
                Rating = 4.9,
                RatingCount = 85,
                CoverImageURL = "/images/courses/knitting-basics-cover.jpeg",
                PromoVideoURL = "https://www.youtube.com/embed/knitting101",
                WideRangeCategory = WideRangeCategory.Textile,
                DateOfCreation = DateTime.UtcNow.AddMonths(-3),
                LastUpdatedTimestamp = DateTime.UtcNow.AddDays(-10),
                Lessons = new List<Lesson>
                {
                    new Lesson 
                    { 
                        Title = "Basic Knitting Stitches", 
                        SequenceOrder = 1, 
                        ContentType = LessonContentType.Video, 
                        ContentData = "https://www.youtube.com/embed/knit-basics", 
                        Description = "Learn the fundamental knitting stitches." 
                    },
                    new Lesson 
                    { 
                        Title = "Your First Scarf Project", 
                        SequenceOrder = 2, 
                        ContentType = LessonContentType.Video, 
                        ContentData = "https://www.youtube.com/embed/scarf-project", 
                        Description = "Create your first knitted scarf." 
                    }
                }
            }
        };
    }
}

