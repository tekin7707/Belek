using FluentValidation;
using Belek.Frontends.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Belek.Frontends.Web.Validators
{
    public class ProductCreateInputValidator : AbstractValidator<CatalogCreateInput>
    {
        public ProductCreateInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("isim alanı boş olamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("açıklama alanı boş olamaz");

            RuleFor(x => x.Price).NotEmpty().WithMessage("fiyat alanı boş olamaz").WithMessage("hatalı para formatı");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("kategori alanı seçiniz");
        }
    }
}