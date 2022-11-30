using AutoMapper;
using SystemOrder.Domain.Models;
using SystemOrder.Resources;

namespace SystemOrder.Mapping
{
	public class ResourceToModelProfile : Profile
	{
		public ResourceToModelProfile()
		{
			CreateMap<SaveProductResource, Product>();

			CreateMap<SaveOrderResource, Order>();

		}
	}
}
