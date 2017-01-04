﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.ActiveDirectory
// 
//  Dapplo.ActiveDirectory is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.ActiveDirectory is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.ActiveDirectory. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.Runtime.Serialization;

#endregion

namespace Dapplo.Confluence.Query
{
	/// <summary>
	///     the functions which can be used as values
	/// </summary>
	public enum Functions
	{
		[EnumMember(Value = "currentUser()")]
		CurrentUser,
		[EnumMember(Value = "endOfDay()")]
		EndOfDay,
		[EnumMember(Value = "endOfMonth()")]
		EndOfMonth,
		[EnumMember(Value = "endOfWeek()")]
		EndOfWeek,
		[EnumMember(Value = "endOfYear()")]
		EndOfYear,
		[EnumMember(Value = "startOfDay()")]
		StartOfDay,
		[EnumMember(Value = "startOfMonth()")]
		StartOfMonth,
		[EnumMember(Value = "startOfWeek()")]
		StartOfWeek,
		[EnumMember(Value = "startOfYear()")]
		StartOfYear,
		[EnumMember(Value = "favouriteSpaces()")]
		FavouriteSpaces,
		[EnumMember(Value = "recentlyViewedContent()")]
		RecentlyViewedContent,
		[EnumMember(Value = "recentlyViewedSpaces()")]
		RecentlyViewedSpaces,
	}
}