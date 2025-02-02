using System;
using UnityEngine;

namespace Game.World {
	public struct WorldTime: IComparable<WorldTime>, IEquatable<WorldTime> {
		public int Day { get; private set; }
		public int Time { get; private set; }
		
		public int Hour => Time / 60;
		public int Minute => Time % 60;

		public event Action TimeChanged;
		public event Action DayChanged;
		
		public const int TimeInDay = 60 * 24;
		
		public WorldTime(int day, int dayTime) {
			Day = Mathf.Max(day, 0);
			Time = Mathf.Clamp(dayTime, 0, TimeInDay);
			TimeChanged = null;
			DayChanged = null;
		}

		public void AddTick() {
			Add(1);
		}
		public void Add(int time) {
			Time += time;
			if (Time >= TimeInDay) {
				Day += Time / TimeInDay;
				Time = Time % TimeInDay;
				DayChanged?.Invoke();
			}
			TimeChanged?.Invoke();
		}
		public void SetTime(int time) {
			Time = Mathf.Max(time, TimeInDay);
			TimeChanged?.Invoke();
		}
		public void SetDay(int day) {
			Day = Mathf.Max(day, 0);
			DayChanged?.Invoke();
		}

		public bool IsAfter(WorldTime time) {
			return this.CompareTo(time) > 0;
		}
		public bool IsBefore(WorldTime time) {
			return this.CompareTo(time) < 0;
		}

		public string TimeToString() {
			return $"{Time / 60:D2}:{Time % 60:D2}";
		}
		public int CompareTo(WorldTime other) {
			if (Day == other.Day) {
				return Time - other.Time;
			}
			return Day - other.Day;
		}
		public bool Equals(WorldTime other) {
			return Day == other.Day && Time == other.Time;
		}
		public override int GetHashCode() {
			return HashCode.Combine(Day, Time);
		}
	}
}