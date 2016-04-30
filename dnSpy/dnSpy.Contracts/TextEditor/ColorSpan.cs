﻿/*
    Copyright (C) 2014-2016 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using dnSpy.Contracts.Themes;

namespace dnSpy.Contracts.TextEditor {
	/// <summary>
	/// Span and color
	/// </summary>
	public struct ColorSpan {
		/// <summary>
		/// Span
		/// </summary>
		public Span Span { get; }

		/// <summary>
		/// Color
		/// </summary>
		public Color Color { get; }

		/// <summary>
		/// Priority
		/// </summary>
		public double Priority { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="span">Span</param>
		/// <param name="color">Color</param>
		/// <param name="priority">Priority</param>
		public ColorSpan(Span span, Color color, double priority = ColorPriority.Normal) {
			Span = span;
			Color = color;
			Priority = priority;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="span">Span</param>
		/// <param name="color">Color</param>
		/// <param name="priority">Priority</param>
		public ColorSpan(Span span, IThemeColor color, double priority = ColorPriority.Normal) {
			if (color == null)
				throw new ArgumentNullException(nameof(color));
			Span = span;
			Color = new Color(color);
			Priority = priority;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="span">Span</param>
		/// <param name="color">Color</param>
		/// <param name="priority">Priority</param>
		public ColorSpan(Span span, ColorType color, double priority = ColorPriority.Normal) {
			Span = span;
			Color = new Color(color);
			Priority = priority;
		}
	}
}
