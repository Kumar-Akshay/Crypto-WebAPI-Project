# API Project 
## _This is a API Project follow the MVC Architecture._
I used the DatabaseFirst Approach to create the database using Entity Framework, and the scaffold command below is used to generate the dbcontext and models using DatabaseFirst. 

Scaffold-DbContext 'server=localhost; port=3306; database=CYRPTODATABASE; user=root; password=password; Persist Security Info=False; Connect Timeout=300' Pomelo.EntityFrameworkCore.MySql -OutputDir Model -force
```

I have implemented the three endpoint 
```sh
- GetAll Crypto Currency
This API Used to retrive all crypto currency with there categories.
- Get CryptoCurrency by Symbol
This API Used to retrive crypto currency with specific symbol.
- Add new Crypto Currency
This API Used to Add new crypto currency.

I have prepare the postman collection. 
https://www.getpostman.com/collections/a6fdd950b0187b025061
