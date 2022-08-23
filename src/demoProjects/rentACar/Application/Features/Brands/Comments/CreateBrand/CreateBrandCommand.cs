using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Comments.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>
    {
        
        public string Name { get; set; }
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            //Readonly tanımlı değişkeni salt okunur moduna getirmektedir. Yani readonly olarak tanımlanan bir değişken sadece okunabilmektedir.
            //Setleme işlemi değişkenin oluşturulduğu anda ya da oluşturulan sınıfın constructor metodu içerisinde yapılmaktadır.
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                //_brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                //await _brandEntityBusinessRules.SomeFeatureEntityNameCanNotBeDuplicatedWhenInserted(request.Name);

                // requesti brande maple veya eşleştir veya dönüştür.
                Brand mappedBrand = _mapper.Map<Brand>(request);
                Brand createdBrandRepository = await _brandRepository.AddAsync(mappedBrand);

                // Dto = data transformation object
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrandRepository);
                return createdBrandDto;
            }
        }



        //public class CreateSomeFeatureEntityCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        //{
        //    private readonly IBrandRepository _brandEntityRepository;
        //    private readonly IMapper _mapper;
        //    private readonly BrandBusinessRules _brandEntityBusinessRules;

        //    public CreateSomeFeatureEntityCommandHandler(IBrandRepository someFeatureEntityRepository, IMapper mapper,
        //                                     BrandBusinessRules someFeatureEntityBusinessRules)
        //    {
        //        _brandEntityRepository = someFeatureEntityRepository;
        //        _mapper = mapper;
        //        _brandEntityBusinessRules = someFeatureEntityBusinessRules;
        //    }

        //    public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        //    {
        //        await _brandEntityBusinessRules.SomeFeatureEntityNameCanNotBeDuplicatedWhenInserted(request.Name);

        //        // outomapperın mapini kullanarak requesti branda mapliyoruz.
        //        Brand mappedBrandEntity = _mapper.Map<Brand>(request);

        //        // daha sonra veritabanına gönderiyoruz.
        //        Brand createdBrandEntity = await _brandEntityRepository.AddAsync(mappedBrandEntity);

        //        // Dto = data transformation object
        //        CreatedBrandDto createdBrandEntityDto = _mapper.Map<CreatedBrandDto>(createdBrandEntity);
        //        return createdBrandEntityDto;
        //    }
        //}
    }
}
