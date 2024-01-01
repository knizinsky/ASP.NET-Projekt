# Dokumentacja Aplikacji Sklepu Spożywczego ASP.NET MVC

## Spis Treści
- [Wstęp](#wstęp)
- [Wymagania Systemowe](#wymagania-systemowe)
- [Instalacja](#instalacja)
- [Konfiguracja](#konfiguracja)
- [Uruchamianie Aplikacji](#uruchamianie-aplikacji)
- [Działanie Aplikacji](#działanie-aplikacji)

## Wstęp
Dokumentacja dotyczy aplikacji Sklepu Spożywczego, która umożliwia zarządzanie produktami, zamówieniami oraz autentykacją użytkowników w kontekście sklepu spożywczego.

## 🖥️ Wymagania Systemowe
Aby uruchomić aplikację, komputer musi spełniać następujące wymagania:

- **System Operacyjny:** Windows, Linux, lub macOS
- **Framework:** .NET 6.0
- **Przeglądarka:** Zalecane korzystanie z najnowszych wersji przeglądarek, takich jak Google Chrome, Mozilla Firefox, lub Microsoft Edge.

## ⬇️ Instalacja
Aby zainstalować aplikację, wykonaj poniższe kroki:

1. Pobierz kod źródłowy z repozytorium GitHub.
2. Otwórz projekt w środowisku programistycznym, na przykład Visual Studio.
3. Zainstaluj wymagane zależności za pomocą menadżera pakietów NuGet.
4. Otwórz SSMS i utwórz bazę danych.
5. Zaktualizuj ustawienia połączenia w pliku appsettings.json, aby wskazywał na poprawną bazę danych.
6. Uruchom aplikację.

## 🛠️ Konfiguracja
### Łańcuch Połączenia z Bazą Danych
Konfiguracja łańcucha połączenia z bazą danych znajduje się w pliku `appsettings.json`. Edytuj sekcję `ConnectionStrings` i dostosuj łańcuch połączenia według potrzeb:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=ACERASPIRE5\\SQLEXPRESS;Database=GroceryStore;Trusted_Connection=True;TrustServerCertificate=True;"
},
```

### 🔑 Dane Testowych dla Użytkowników
W pliku `ApplicationDbContext.SeedData` znajdują się dane testowe dla administratora. Domyślne dane to:

- **Email:** admin@example.com
- **Nazwa użytkownika:** admin
- **Hasło:** Password123!

### ⚙️ Konfiguracja Polityk Hasła
W pliku `Startup.cs` znajduje się konfiguracja polityk hasła dla użytkowników. Domyślne ustawienia to brak wymogu cyfry, minimalna długość hasła wynosząca 6 znaków oraz brak innych wymagań.

## ▶️ Uruchamianie Aplikacji
Aplikację można uruchomić, korzystając z dowolnego środowiska programistycznego obsługującego .NET 6.0. W przypadku korzystania z Visual Studio, kliknij przycisk "Start" lub użyj poniższej komendy w terminalu:

```bash
dotnet run
```

## 🖥️ Działanie Aplikacji
Aplikacja składa się z kilku głównych funkcji:

- **Produkty:** Zarządzanie produktami obejmuje dodawanie, edytowanie, usuwanie i przeglądanie szczegółów produktów.
- **Zamówienia:** Funkcje dotyczące zamówień obejmują przeglądanie wszystkich zamówień, dodawanie nowych zamówień, edytowanie istniejących oraz usuwanie zamówień.
- **Autentykacja Użytkowników:** Aplikacja obsługuje autentykację użytkowników, umożliwiając im logowanie i wylogowywanie się.
