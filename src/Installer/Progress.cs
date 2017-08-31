// -----------------------------------------------------------------------
// <copyright file="Progress.cs" company="Ollon, LLC">
//     Copyright (c) 2017 Ollon, LLC. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ExtensionEssentials
{
    public class Progress
    {
        public Progress(int totalCount)
        {
            TotalCount = totalCount;
        }

        public string Text { get; set; }

        public float TotalCount { get; }

        public float Current { get; set; }

        public int Percent
        {
            get
            {
                if (Current == 0 || TotalCount == 0)
                {
                    return 0;
                }
                return (int) (Current / TotalCount * 100);
            }
        }
    }
}
