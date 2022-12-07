using CinemaMvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CinemaMvc.Data.EnumClasses;

namespace CinemaMvc.Data
{
    public class AppDbinitializtion
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //Cinemas
                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema
                        {
                        Name ="Cinema 1",
                        Discription="This is the discription of the first cinema",
                        CinemaLogo = "~/images/cinemas/images - 2022-11-16T172124.128.jpeg"
                        },
                       new Cinema
                        {
                        Name ="Cinema 2",
                        Discription="This is the discription of the second cinema",
                        CinemaLogo = "~/images/cinemas/images (100).jpeg"
                        },
                        new Cinema
                        {
                        Name ="Cinema 3",
                        Discription="This is the discription of the third cinema",
                        CinemaLogo = "~/images/cinemas/images (2).png"
                        },
                        new Cinema
                        {
                        Name ="Cinema 4",
                        Discription="This is the discription of the fourth cinema",
                        CinemaLogo = "~/images/cinemas/images (91).jpeg"
                        },
                         new Cinema
                        {
                        Name ="Cinema 5",
                        Discription="This is the discription of the fifth cinema",
                        CinemaLogo = "~/images/cinemas/images (94).jpeg"
                        },
                          new Cinema
                        {
                        Name ="Cinema 5",
                        Discription="This is the discription of the fifth cinema",
                        CinemaLogo = "~/images/cinemas/images (95).jpeg"
                        },

                    });

                }
                context.SaveChanges();
                //Actors
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName ="Actor 1",
                            Bio="this is Bio of the first Actor",
                            ProfilePictureURL ="~/images/actors/chris evans.jpeg"
                        },
                         new Actor()
                        {
                            FullName ="Actor 2",
                            Bio="this is Bio of the second Actor",
                            ProfilePictureURL ="~/images/actors/images - 2022-11-16T170844.191.jpeg"
                        },
                         new Actor()
                        {
                            FullName ="Actor 3",
                            Bio="this is Bio of the third Actor",
                            ProfilePictureURL ="~/images/actors/images - 2022-11-16T170852.552.jpeg"
                        },
                          new Actor()
                        {
                            FullName ="Actor 4",
                            Bio="this is Bio of the fouth Actor",
                            ProfilePictureURL ="~/images/actors/images - 2022-11-16T170906.002.jpeg"
                        },
                           new Actor()
                        {
                            FullName ="Actor 5",
                            Bio="this is Bio of the fifth Actor",
                            ProfilePictureURL ="~/images/actors/images - 2022-11-16T170925.292.jpeg"
                        },
                            new Actor()
                        {
                            FullName ="Actor 6",
                            Bio="this is Bio of the six Actor",
                            ProfilePictureURL ="~/images/actors/Robert Downey.jpeg"
                        },
                             new Actor()
                        {
                            FullName ="Actor 7",
                            Bio="this is Bio of the seventh Actor",
                            ProfilePictureURL ="~/images/actors/mark wahlber.jpeg"
                        },

                    });

                }
                context.SaveChanges();
                //Producer
                if (!context.producers.Any())
                {
                    context.producers.AddRange(new List<Producer>()
                    {
                          new Producer()
                        {
                            FullName ="Producer 1",
                            Bio="this is Bio of the first producer",
                            ProfilePictureURL ="~/images/producers/images - 2022-11-16T170641.752.jpeg"
                        },
                            new Producer()
                        {
                            FullName ="Producer 2",
                            Bio="this is Bio of the second producer",
                            ProfilePictureURL ="~/images/producers/images - 2022-11-16T170650.552.jpeg"
                        },
                              new Producer()
                        {
                            FullName ="Producer 3",
                            Bio="this is Bio of the third producer",
                            ProfilePictureURL ="~/images/producers/images - 2022-11-16T170658.133.jpeg"
                        },
                                new Producer()
                        {
                            FullName ="Producer 4",
                            Bio="this is Bio of the fourth producer",
                            ProfilePictureURL ="~/images/producers/images - 2022-11-16T170717.254.jpeg"
                        },
                                  new Producer()
                        {
                            FullName ="Producer 5",
                            Bio="this is Bio of the fifht producer",
                            ProfilePictureURL ="~/images/producers/images - 2022-11-16T170816.832.jpeg"
                        },
                    });

                }
                context.SaveChanges();
                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                new Movie()
                {
                    Name ="Life",
                    Discription ="this is the Shawshank Redemption description",
                    Price = 35,
                    ImageURL = "~/images/films/images - 2022-11-16T170307.121.jpeg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3),
                    CinemaId=1,
                    ProducerId =3,
                    MovieCategory = "2"
                },
                new Movie()
                {
                    Name ="The Shawshank Redemption",
                    Discription ="this is the Shawshank Redemption description",
                    Price = 29,
                    ImageURL = "~/images/films/images - 2022-11-16T170456.602.jpeg",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3),
                    CinemaId=2,
                    ProducerId =1,
                    MovieCategory = "3"
                },
                    new Movie()
                {
                    Name ="Ghost",
                    Discription ="this is the Ghost movie description",
                    ImageURL = "~/images/films/images (94).jpeg",
                    Price = 39,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(3),
                    CinemaId=4 ,
                    ProducerId =4,
                    MovieCategory = "2"
                },
                    new Movie()
                {
                    Name ="Race",
                    Discription ="this is the Race description",
                    ImageURL = "~/images/films/download (2).jpeg",
                    Price = 43,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-5),
                    CinemaId=1 ,
                    ProducerId =2,
                    MovieCategory = "4"
                },
                new Movie()
                {
                    Name ="Colb Souls ",
                    Discription ="this is the Scoob movie description",
                    ImageURL = "~/images/films/images - 2022-11-16T170347.422.jpeg",
                    Price = 34,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-2),
                    CinemaId=1 ,
                    ProducerId =4,
                    MovieCategory = "1"
                },
                new Movie()
                {
                    Name ="Fantastic Beasts",
                    Discription ="this is the Cold Soles movie description",
                    ImageURL = "~/images/films/Fantastic_Beasts_and_Where_to_Find_Them_poster.png",
                    Price = 25,
                    StartDate = DateTime.Now.AddDays(3),
                    EndDate = DateTime.Now.AddDays(20),
                    CinemaId=4,
                    ProducerId =5,
                    MovieCategory = "3"
                },
                new Movie()
                {
                    Name ="Inception",
                    Discription ="this is the Scoob movie description",
                    ImageURL = "~/images/films/images - 2022-11-16T170610.231.jpeg",
                    Price = 38,
                    StartDate = DateTime.Now.AddDays(-10),
                    EndDate = DateTime.Now.AddDays(-2),
                    CinemaId=3 ,
                    ProducerId =1,
                    MovieCategory = "2"
                },
                new Movie()
                {
                    Name ="Woman In The Chair",
                    Discription ="this is the Scoob movie description",
                    ImageURL = "~/images/films/images (95).jpeg",
                    Price = 39,
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(2),
                    CinemaId=1 ,
                    ProducerId =2,
                    MovieCategory = "1"
                }


                    });
                }
                context.SaveChanges();
                //Actors_Movies 
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actors_Movies>()
                    {
                        new Actors_Movies
                        {
                            ActorId =1,
                            MovieId =1
                        },
                         new Actors_Movies
                        {
                            ActorId =3,
                            MovieId =1
                        },
                          new Actors_Movies
                        {
                            ActorId =6,
                            MovieId =2
                        },
                           new Actors_Movies
                        {
                            ActorId =2,
                            MovieId =5
                        },
                            new Actors_Movies
                        {
                            ActorId =5,
                            MovieId =1
                        },
                             new Actors_Movies
                        {
                            ActorId =3,
                            MovieId =2
                        },
                             new Actors_Movies
                        {
                            ActorId =7,
                            MovieId =3
                        },
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
