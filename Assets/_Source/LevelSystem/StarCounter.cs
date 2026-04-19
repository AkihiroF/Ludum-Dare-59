using System;

namespace LevelSystem
{
    public static class StarCounter
    {
        public static int CurrentIndexLevel { get; private set; }
        public static int CurrentCountStar { get; private set; }

        public static Action OnCountChanged;
        
        

        public static void AddStar()
        {
            CurrentCountStar++;
            OnCountChanged?.Invoke();
        }

        public static void UpdateIndexLevel() => CurrentIndexLevel++;

        public static void Clear()
        {
            CurrentIndexLevel = 0;
            CurrentCountStar = 0;
            OnCountChanged?.Invoke();
        }
    }
}