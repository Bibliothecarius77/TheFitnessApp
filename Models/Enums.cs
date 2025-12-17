/*
 * ZenMove - The Ultimate Fitness App
 *
 * IT-påbyggnad Utvecklare (Lexicon)
 * Kursvecka 13-16 (vv.2551-2602)
 *
 * Grupp Blå:
 *   Arsalan Habib
 *   Jacob Damm
 *   Liridona Demaj
 *   Victoria Rådberg
 */

namespace TheFitnessApp.Models
{
    public enum UserRole
    {
        Admin = 0,
        Regular = 2
    }

    public enum ExerciseType
    {
        Balance = 0,
        Cardio = 1,
        Flexibility = 2,
        Strength = 3
    }

    public enum GoalType
    {
        WeightLoss = 0,
        MuscleGain = 1,
        Endurance = 2,
        WorkoutFrequency = 3
    }
}
