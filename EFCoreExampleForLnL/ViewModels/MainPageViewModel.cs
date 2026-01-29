using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFCoreExampleForLnL.Data;
using EFCoreExampleForLnL.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExampleForLnL.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    public ObservableCollection<Product> Products { get; } = new();
    public ObservableCollection<Customer> Customers { get; } = new();
    public ObservableCollection<Address> Addresses { get; } = new();

    private Customer? _selectedCustomer;
    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            if (SetProperty(ref _selectedCustomer, value))
            {
                ApplyFilters();
            }
        }
    }

    private Address? _selectedAddress;
    public Address? SelectedAddress
    {
        get => _selectedAddress;
        set
        {
            if (SetProperty(ref _selectedAddress, value))
            {
                ApplyFilters();
            }
        }
    }

    private List<Product> _allProducts = new();

    public MainPageViewModel(AppDbContext context)
    {
        _context = context;
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        await _context.Database.EnsureCreatedAsync();

        _allProducts = await _context.Products
            .Include(p => p.Customer)
            .ThenInclude(c => c.Address)
            .ToListAsync();

        var customers = await _context.Customers
            .Include(c => c.Address)
            .ToListAsync();

        var addresses = await _context.Addresses.ToListAsync();

        Customers.Clear();
        Customers.Add(new Customer { Id = 0, Name = "All Customers" });
        foreach (var customer in customers)
        {
            Customers.Add(customer);
        }

        Addresses.Clear();
        Addresses.Add(new Address { Id = 0, City = "All Addresses" });
        foreach (var address in addresses)
        {
            Addresses.Add(address);
        }

        SelectedCustomer = Customers.FirstOrDefault();
        SelectedAddress = Addresses.FirstOrDefault();

        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var filtered = _allProducts.AsEnumerable();

        if (SelectedCustomer != null && SelectedCustomer.Id != 0)
        {
            filtered = filtered.Where(p => p.CustomerId == SelectedCustomer.Id);
        }

        if (SelectedAddress != null && SelectedAddress.Id != 0)
        {
            filtered = filtered.Where(p => p.Customer.AddressId == SelectedAddress.Id);
        }

        Products.Clear();
        //The reason we can't use AddRange is because it's a obeservable collection that implements INotifyCollectionChanged, INotifyPropertyChanged.
        //Meaning it's not really usefull if it's going to be firing off all those events for the GUI anyways.

        //ASG.UserInterface in our library uses our own implementation of ObservableCollection calls https://dev.azure.com/associatedsteelgroup/ASG/_git/ASG.UserInterface?path=/ASG.UserInterface/Extensions/CustObsCollection.cs
        foreach (var product in filtered)
        {
            Products.Add(product);
        }
    }
}
