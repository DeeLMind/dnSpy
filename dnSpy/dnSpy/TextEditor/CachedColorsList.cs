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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using dnSpy.Shared.Highlighting;

namespace dnSpy.TextEditor {
	sealed class CachedColorsList {
		readonly List<OffsetAndCachedColors> cachedColorsList = new List<OffsetAndCachedColors>();

		public OffsetAndCachedColors Find(int docOffset) {
			for (int i = 0; i < cachedColorsList.Count; i++) {
				var info = cachedColorsList[(previousReturnedIndex + i) % cachedColorsList.Count];
				if ((info.CachedColors.Length == 0 && info.Offset == docOffset) || (info.Offset <= docOffset && docOffset < info.Offset + info.CachedColors.Length)) {
					previousReturnedIndex = i;
					return info;
				}
			}

			return OffsetAndCachedColors.Default;
		}
		int previousReturnedIndex;

		public void Add(int offset, CachedTextTokenColors cachedColors) {
			Debug.Assert((cachedColorsList.Count == 0 && offset == 0) || (cachedColorsList.Count > 0 && cachedColorsList.Last().Offset + cachedColorsList.Last().CachedColors.Length <= offset));
			cachedColorsList.Add(new OffsetAndCachedColors(offset, cachedColors));
		}

		public void SetAsyncUpdatingAfterChanges(int docOffset) =>
			AddOrUpdate(docOffset, OffsetAndCachedColors.Default.CachedColors);

		public void AddOrUpdate(int docOffset, CachedTextTokenColors newCachedColors) {
			for (int i = 0; i < cachedColorsList.Count; i++) {
				int mi = (previousReturnedIndex + i) % cachedColorsList.Count;
				var info = cachedColorsList[mi];
				if (info.Offset == docOffset) {
					cachedColorsList[mi] = new OffsetAndCachedColors(docOffset, newCachedColors);
					return;
				}
			}
			Add(docOffset, newCachedColors);
		}

		public CachedTextTokenColors RemoveLastCachedTextTokenColors() {
			Debug.Assert(cachedColorsList.Count > 0);
			if (cachedColorsList.Count == 0)
				return null;
			int index = cachedColorsList.Count - 1;
			var info = cachedColorsList[index];
			cachedColorsList.RemoveAt(index);
			return info.CachedColors;
		}

		public void Clear() => cachedColorsList.Clear();
	}
}
