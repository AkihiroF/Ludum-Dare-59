using System;

namespace LevelSystem
{
    public static class StarCounter
    {
        public static int CurrentCountStar { get; private set; }

        public static Action OnCountChanged;

        public static void AddStar()
        {
            CurrentCountStar++;
            OnCountChanged?.Invoke();
        }

        public static void Clear()
        {
            CurrentCountStar = 0;
            OnCountChanged?.Invoke();
        }
    }
}