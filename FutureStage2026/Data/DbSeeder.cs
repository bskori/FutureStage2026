using FutureStage2026.Models;

namespace FutureStage2026.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // 🔹 Education Boards
            if (!context.EducationBoards.Any())
            {
                context.EducationBoards.AddRange(
                    new EducationBoard { EducationBoardTitle = "CBSE" },
                    new EducationBoard { EducationBoardTitle = "ICSE" },
                    new EducationBoard { EducationBoardTitle = "State Board" },
                    new EducationBoard { EducationBoardTitle = "IB" }
                );
            }

            // 🔹 Mediums
            if (!context.Mediums.Any())
            {
                context.Mediums.AddRange(
                    new Medium { MediumTitle = "English" },
                    new Medium { MediumTitle = "Hindi" },
                    new Medium { MediumTitle = "Marathi" }
                );
            }

            // 🔹 Facilities
            if (!context.Facilities.Any())
            {
                context.Facilities.AddRange(
                    new Facility { FacilityTitle = "Library" },
                    new Facility { FacilityTitle = "Computer Lab" },
                    new Facility { FacilityTitle = "Sports Ground" },
                    new Facility { FacilityTitle = "Transport" },
                    new Facility { FacilityTitle = "Cafeteria" },
                    new Facility { FacilityTitle = "Smart Classes" }
                );
            }

            context.SaveChanges();
        }
    }
}