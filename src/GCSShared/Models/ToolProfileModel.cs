﻿
namespace GSendShared.Models
{
    public sealed class ToolProfileModel : IToolProfile
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
