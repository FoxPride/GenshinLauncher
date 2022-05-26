using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GenshinLauncher.Models
{
    public sealed class Resolution : IEquatable<Resolution>
    {
        public static readonly ObservableCollection<Resolution> Presets = new()
        {
            new Resolution(640, 480),
            new Resolution(720, 400),
            new Resolution(720, 480),
            new Resolution(800, 600),
            new Resolution(1024, 768),
            new Resolution(1152, 864),
            new Resolution(1176, 664),
            new Resolution(1280, 720),
            new Resolution(1280, 768),
            new Resolution(1280, 800),
            new Resolution(1280, 960),
            new Resolution(1280, 1024),
            new Resolution(1360, 768),
            new Resolution(1366, 768),
            new Resolution(1440, 900),
            new Resolution(1600, 900),
            new Resolution(1600, 1024),
            new Resolution(1680, 1050),
            new Resolution(1920, 1080),
            new Resolution(3840, 2160)
        };

        [JsonConstructor]
        private Resolution(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Height { get; }
        public int Width { get; }

        public static Resolution GetResolution(int width, int height)
        {
            var other = new Resolution(width, height);
            var resolution = Presets.FirstOrDefault(r => r.Equals(other));
            if (resolution != null)
            {
                return resolution;
            }
            Presets.Add(other);
            return other;
        }

        public bool Equals(Resolution? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Height == other.Height && Width == other.Width;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Resolution)obj);
        }

        public override int GetHashCode() => HashCode.Combine(Height, Width);

        public override string ToString() => $"{Width}x{Height}";
    }
}