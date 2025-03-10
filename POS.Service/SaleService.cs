using AutoMapper;
using POS.Core.Dtos.SaleDTOs;
using POS.Core.Dtos.SaleItemDTOs;
using POS.Core.Dtos;
using POS.Core.Repository;
using POS.Core.Service;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemRepository _saleItemRepository;
    private readonly IProductRepository _productRepository;
    private readonly ITaxRepository _taxRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public SaleService(
        ISaleRepository saleRepository,
        ISaleItemRepository saleItemRepository,
        IProductRepository productRepository,
        ITaxRepository taxRepository,
        IDiscountRepository discountRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _saleItemRepository = saleItemRepository;
        _productRepository = productRepository;
        _taxRepository = taxRepository;
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<SaleReceiptDto> CreateSaleAsync(SaleCreateDto saleDto)
    {
        decimal subtotal = 0;
        decimal discountAmount = 0;
        decimal taxAmount = 0;
        var saleItems = new List<SaleItem>();

        foreach (var item in saleDto.SaleItems)
        {
            var product = await _productRepository.GetProductAsync(item.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {item.ProductId} not found.");

            decimal itemTotal = product.Price * item.Quantity;
            subtotal += itemTotal;

            saleItems.Add(new SaleItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            });
        }

        if (saleDto.DiscountId.HasValue)
        {
            var discount = await _discountRepository.GetDiscountAsync(saleDto.DiscountId.Value);
            if (discount != null)
            {
                discountAmount = (discount.Percentage / 100) * subtotal;
            }
        }

        if (saleDto.TaxId.HasValue)
        {
            var tax = await _taxRepository.GetTaxAsync(saleDto.TaxId.Value);
            if (tax != null)
            {
                taxAmount = (tax.TaxPercentage / 100) * (subtotal - discountAmount);
            }
        }

        decimal totalAmount = subtotal - discountAmount + taxAmount;

        var sale = new Sale
        {
            UserId = saleDto.UserId,
            SaleDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            DiscountId = saleDto.DiscountId,
            TaxId = saleDto.TaxId,
            SaleItems = saleItems
        };

        var savedSale = await _saleRepository.CreateSaleAsync(sale);
        await _saleItemRepository.AddSaleItemsAsync(saleItems);

        return new SaleReceiptDto
        {
            SaleId = savedSale.SaleId,
            SaleDate = savedSale.SaleDate,
            SubTotal = subtotal,
            DiscountAmount = discountAmount,
            TaxAmount = taxAmount,
            TotalAmount = savedSale.TotalAmount,
            SaleItems = _mapper.Map<List<SaleItemDto>>(saleItems)
        };
    }

    public async Task<SaleDto?> GetSaleAsync(int saleId)
    {
        var sale = await _saleRepository.GetSaleAsync(saleId);
        return sale != null ? _mapper.Map<SaleDto>(sale) : null;
    }

    public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
    {
        var sales = await _saleRepository.GetAllSalesAsync();
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }
}
