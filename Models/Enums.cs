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
        Admin,
        User
    }

    public enum ExerciseType
    {
        Balance,
        Cardio,
        Flexibility,
        Strength
    }

    public enum GoalType
    {
        WeightLoss,
        MuscleGain,
        Endurance,
        WorkoutFrequency
    }
}
