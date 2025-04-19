Pasos:

finalmente correrlo todo

dotnet run

+

npm run dev

# Instalación

Requisitos: dotnet 8, dotnet ef, python 3.13, npm, node

# Migraciones y seeds

migrar backend con dotnet ef (dotnet ef database update)

Agregar datos de los seeds (python migrate.py)

# Frontend
Correr con npm run dev

# Backend
Correr con dotnet run

## Swagger


# Scripts de seeding

Correr con python migrate.py

# Patron de diseño
El patrón de diseño a comentar es el patrón builder, presente en el script de data_migration. La clase en cuestión es la clase `DataLoader`, que corresponde a un constructor que a partir de un path de archivo genera las entidades a guardar en la base de datos. Se puede extender la interfaz de esta api para tener otras restricciones (por ejemplo, que no se descarten las facturas invalidas), o cambiar la representación en la que se guardan los objetos para así poder entrar en otros esquemas de bases de datos si los hubiera.

Además, se usan patrones strategy para validar los nuevos credit notes y las facturas, que son INewCreditNoteValidator y IInvoiceValidator

# Suposiciones

* El credit_note_number es asignado automáticamente y no es ingresado por el usuario.