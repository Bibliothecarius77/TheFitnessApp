/*
 * ZenMove - The Ultimate Fitness App
 *
 * IT-påbyggnad Utvecklare, Fullstack .NET (Lexicon)
 * Kursvecka 13-16 (vv.2551-2602)
 *
 * Grupp Blå:
 *   Arsalan Habib
 *   Jacob Damm
 *   Liridona Demaj
 *   Victoria Rådberg
 */

using System.ComponentModel.DataAnnotations;

namespace TheFitnessApp.Models
{
    public class ExerciseInputModel
    {
        public ExerciseType Type { get; set; }
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Exercise sets cannot be a negative number.")]
        public int Sets { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Exercise repetitions cannot be a negative number.")]
        public int Reps { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Exercise weight (kg) cannot be a negative number.")]
        public int WeightKG { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Exercise calories burnt cannot be a negative number.")]
        public int CaloriesBurnt { get; set; }

        [Range(0.0f, float.PositiveInfinity, ErrorMessage = "Exercise MET value cannot be a negative number.")]
        public float METValue { get; set; }
    }

    public class Exercise
    {
        public Guid ExerciseID { get; set; }               // Primary Key
        public WorkoutSession Session
        {
            get
            {
                return Session;
            }
            set
            {
                // Set Foreign Key from this entity
                SessionID = value.SessionID;
            }
        }
        public Guid SessionID { get; set; }                // Foreign Key
        public ExerciseType Type { get; set; }
        public string? Category { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int WeightKG { get; set; }
        public int CaloriesBurnt { get; set; }
        public float METValue { get; set; }

        // Constructor for EF Core
        public Exercise()
        {
        }

        public Exercise(ExerciseType type, string? category, int sets, int reps, int weightKG, int caloriesBurt, float metValue)
        {
            Update(type, category, sets, reps, weightKG, caloriesBurt, metValue);
        }

        //public Exercise(WorkoutSession session, Guid id, ExerciseType type, string? category, int sets, int reps, int weightKG, int caloriesBurt, float metValue)
        //{
        //    Session = session;
        //    SessionID = id;
        //    Update(type, category, sets, reps, weightKG, caloriesBurt, metValue);
        //}

        public void Update(ExerciseType type, string? category, int sets, int reps, int weightKG, int caloriesBurt, float metValue)
        {
            Type = type;
            Category = category;
            Sets = sets;
            Reps = reps;
            WeightKG = weightKG;
            CaloriesBurnt = caloriesBurt;
            METValue = metValue;
        }
    }
}
