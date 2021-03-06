//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Confluence
// 
//  Dapplo.Confluence is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Confluence is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Confluence. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.Confluence.Entities;
using Dapplo.HttpExtensions;

#endregion

namespace Dapplo.Confluence
{
	/// <summary>
	///     Marker interface for the space related methods
	/// </summary>
	public interface ISpaceDomain : IConfluenceDomain
	{
	}

	/// <summary>
	///     All space related extension methods
	/// </summary>
	public static class SpaceExtensions
	{
		/// <summary>
		///     Create a space
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="key">Key for the space</param>
		/// <param name="name">Name for the space</param>
		/// <param name="description">Description for the space</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>created Space</returns>
		public static async Task<Space> CreateAsync(this ISpaceDomain confluenceClient, string key, string name, string description, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var space = new Space
			{
				Key = key,
				Name = name,
				Description = new Description
				{
					Plain = new Plain
					{
						Value = description
					}
				}
			};
			var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space");
			var response = await spaceUri.PostAsync<HttpResponse<Space, Error>>(space, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Create a private space
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="key">Key for the space</param>
		/// <param name="name">Name for the space</param>
		/// <param name="description">Description for the space</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>created Space</returns>
		public static async Task<Space> CreatePrivateAsync(this ISpaceDomain confluenceClient, string key, string name, string description, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var space = new Space
			{
				Key = key,
				Name = name,
				Description = new Description
				{
					Plain = new Plain
					{
						Value = description
					}
				}
			};
			var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", "_private");
			var response = await spaceUri.PostAsync<HttpResponse<Space, Error>>(space, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Delete a space
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="key">Key for the space</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Long running task, which takes care of deleting the space</returns>
		public static async Task<LongRunningTask> DeleteAsync(this ISpaceDomain confluenceClient, string key, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", key);
			var response = await spaceUri.DeleteAsync<HttpResponse<LongRunningTask>>(cancellationToken).ConfigureAwait(false);
			if (response.StatusCode == HttpStatusCode.Accepted)
			{
				return response.Response;
			}
			throw new Exception(response.StatusCode.ToString());
		}

		/// <summary>
		///     Get Spaces see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Spaces</returns>
		public static async Task<IList<Space>> GetAllAsync(this ISpaceDomain confluenceClient, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var spacesUri = confluenceClient.ConfluenceApiUri.AppendSegments("space");

			if (ConfluenceClientConfig.ExpandGetSpace != null && ConfluenceClientConfig.ExpandGetSpace.Length != 0)
			{
				spacesUri = spacesUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetSpace));
			}

			var response = await spacesUri.GetAsAsync<HttpResponse<Result<Space>, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response.Results;
		}

		/// <summary>
		///     Get Space information see <a href="https://docs.atlassian.com/confluence/REST/latest/#d3e164">here</a>
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="spaceKey">the space key</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Space</returns>
		public static async Task<Space> GetAsync(this ISpaceDomain confluenceClient, string spaceKey, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", spaceKey);
			if (ConfluenceClientConfig.ExpandGetSpace != null && ConfluenceClientConfig.ExpandGetSpace.Length != 0)
			{
				spaceUri = spaceUri.ExtendQuery("expand", string.Join(",", ConfluenceClientConfig.ExpandGetSpace));
			}

			var response = await spaceUri.GetAsAsync<HttpResponse<Space, Error>>(cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse.Message);
			}
			return response.Response;
		}

		/// <summary>
		///     Update a space
		/// </summary>
		/// <param name="confluenceClient">ISpaceDomain to bind the extension method to</param>
		/// <param name="space">Space to update</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>created Space</returns>
		public static async Task<Space> UpdateAsync(this ISpaceDomain confluenceClient, Space space, CancellationToken cancellationToken = default(CancellationToken))
		{
			confluenceClient.Behaviour.MakeCurrent();
			var spaceUri = confluenceClient.ConfluenceApiUri.AppendSegments("space", space.Key);
			var response = await spaceUri.PutAsync<HttpResponse<Space, string>>(space, cancellationToken).ConfigureAwait(false);
			if (response.HasError)
			{
				throw new Exception(response.ErrorResponse);
			}
			return response.Response;
		}
	}
}