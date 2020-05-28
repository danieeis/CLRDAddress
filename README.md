# Introduction 

This is a .NET Standard addressing library for get the countries powered by CLRD (Unicode Common Locale Data Repository) v37 and addreesses sensitive by GAD (Google address data)

Features:

- Countries, with translations for over 250 locales
- Address formats for over 200 countries
- Subdivisions (administrative areas, localities, dependent localities)
- Both latin and local subdivision names, when relevant (e.g: Okinawa / 沖縄県)

The dataset is  [stored locally](https://github.com/danieeis/CLRDAddress/tree/master/CLRDGAddress/CLRDGAddress/CLRD) in XML format. Countries are generated from [CLDR](http://cldr.unicode.org) v37. Address formats and subdivisions are generated from Google [Address Data Service](https://chromium-i18n.appspot.com/ssl-address).

[Document](https://github.com/google/libaddressinput/wiki/AddressValidationMetadata) that explains how to use Google's open-source address metadata to validate addresses.

# Setup

 - Available on NuGet https://www.nuget.org/packages/CLRDGAddress [![NuGet](https://img.shields.io/nuget/v/CLRDGAddress.svg?label=NuGet)](https://www.nuget.org/packages/CLRDGAddress)

# Build

 - [![Build Status](https://dev.azure.com/jdanieltovart/packages/_apis/build/status/CLRDAddress%20pipeline?branchName=master)](https://dev.azure.com/jdanieltovart/packages/_build/latest?definitionId=1&branchName=master)

# API Usage

To use this library, add a using statement for CLRDGAddress and then can 
call CLRDGAddress.Countries.CountriesByLanguage("es") or CLRDGAddress.AddressData.GetAddresses("US")

# Contribute

Want To Support This Project?
Submit bugs, features, and sending those pull requests down!

# Base on Work

[PHP Addressing Library](https://github.com/commerceguys/addressing)

