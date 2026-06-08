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

            // FeeHeads
            if (!context.FeeHeads.Any())
            {
                context.FeeHeads.AddRange(
                    new FeeHead { FeeHeadTitle = "Tuition Fee", FeeHeadDesc = "Monthly tuition charges" },
                    new FeeHead { FeeHeadTitle = "Transport Fee", FeeHeadDesc = "Bus/transport charges" },
                    new FeeHead { FeeHeadTitle = "Admission Fee", FeeHeadDesc = "One-time admission fee" }
                );
            }

            // Standards
            if (!context.Standards.Any())
            {
                context.Standards.AddRange(
                    new Standard { StandardTitle = "Nursery", StandardDesc = "Pre-primary level" },
                    new Standard { StandardTitle = "KG", StandardDesc = "Kindergarten" },
                    new Standard { StandardTitle = "1st", StandardDesc = "Grade 1" },
                    new Standard { StandardTitle = "2nd", StandardDesc = "Grade 2" }
                );
            }

            // 🔹 School Standards Mapping
            
            if (!context.SchoolStandards.Any())
            {
                var school = context.Schools.FirstOrDefault();
                var standards = context.Standards.ToList();

                if (school != null && standards.Any())
                {
                    foreach (var std in standards)
                    {
                        context.SchoolStandards.Add(new SchoolStandard
                        {
                            SchoolId = school.Id,
                            StandardId = std.Id
                        });
                    }
                }

                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}