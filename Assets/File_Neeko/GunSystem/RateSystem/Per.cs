using System;
using System.Collections.Generic;
using UnityEngine;

namespace Neeko {

	public class PerBase {

		[SerializeField]
		protected float _rate;

	}

	[Serializable]
	public class Per<TUnit> : PerBase where TUnit : IPerUnit, new() {

		//======================================================================| Fields

		private readonly TUnit _unit;

		private static readonly Dictionary<Type, IPerUnit> _units = new();

		//======================================================================| Properties

		public float Interval { get; }

		//======================================================================| Constructors

		public Per(float rate) {

			_unit = CacheIfNeeded<TUnit>();
			Interval = _unit.BaseMultiply / rate;

		}

		//======================================================================| Methods

		private static T CacheIfNeeded<T>() where T : IPerUnit, new() {
			
			Type unitType = typeof(T);

			if (!_units.TryGetValue(unitType, out var unit)) {

				unit = new T();
				_units.Add(unitType, unit);

			}

			return (T)unit;

		}

		public Per<TOtherUnit> ConvertTo<TOtherUnit>() where TOtherUnit : IPerUnit, new() {

			var otherUnit = CacheIfNeeded<TOtherUnit>();
			var multiple = otherUnit.BaseMultiply / _unit.BaseMultiply;

			return new(_rate * multiple);

		}

		//======================================================================| Casters

		public static implicit operator float(Per<TUnit> unit) => unit._rate;
		public static implicit operator Per<TUnit>(float value) => new(value);

		//======================================================================| Operators

		public static Per<TUnit> operator+(float value, Per<TUnit> unit) => new(value + unit._rate);
		public static Per<TUnit> operator-(float value, Per<TUnit> unit) => new(value - unit._rate);
		public static Per<TUnit> operator*(float value, Per<TUnit> unit) => new(value * unit._rate);
		public static Per<TUnit> operator/(float value, Per<TUnit> unit) => new(value / unit._rate);
		public static Per<TUnit> operator%(float value, Per<TUnit> unit) => new(value % unit._rate);

		public static Per<TUnit> operator+(Per<TUnit> unit, float value) => new(value + unit._rate);
		public static Per<TUnit> operator-(Per<TUnit> unit, float value) => new(value - unit._rate);
		public static Per<TUnit> operator*(Per<TUnit> unit, float value) => new(value * unit._rate);
		public static Per<TUnit> operator/(Per<TUnit> unit, float value) => new(value / unit._rate);
		public static Per<TUnit> operator%(Per<TUnit> unit, float value) => new(value % unit._rate);

		public static Per<TUnit> operator+(Per<TUnit> left, Per<TUnit> right) => new(left._rate + right._rate);
		public static Per<TUnit> operator-(Per<TUnit> left, Per<TUnit> right) => new(left._rate - right._rate);
		public static Per<TUnit> operator*(Per<TUnit> left, Per<TUnit> right) => new(left._rate * right._rate);
		public static Per<TUnit> operator/(Per<TUnit> left, Per<TUnit> right) => new(left._rate / right._rate);
		public static Per<TUnit> operator%(Per<TUnit> left, Per<TUnit> right) => new(left._rate % right._rate);

	}	

}