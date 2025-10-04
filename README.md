# Desafio Prático: POO em C# na Prática

Este repositório contém uma solução estruturada para um conjunto de desafios envolvendo Programação Orientada a Objetos (POO) em C#, com foco em boas práticas como Nullability (NRT), associações, exceções, métodos estáticos e conceitos de design como Value Objects e Injeção de Dependência.

## Como o código está organizado

* **Domain** – contém entidades do domínio e objetos de valor. Por exemplo:
  * `DomainException` encapsula violações de regras de negócio.
  * `Money` é um Value Object para representar valores monetários com moeda e operações seguras (adição, subtração e multiplicação).
  * `Product` possui validação para nome, preço e estoque. Lança `ArgumentOutOfRangeException` para valores negativos e `DomainException` quando não há estoque suficiente.
  * `OrderItem` representa um item de um pedido e calcula seu subtotal.
  * `Order` agrupa itens e calcula o total, evitando duplicidade de produtos na lista.
* **Utils** – inclui métodos estáticos e extensões. Por exemplo, `MathExtensions` oferece `Square` e `Clamp` como extensões de `int`.
* **Program.cs** – demonstra o uso das classes acima, ilustrando o tratamento de exceções, operações com o Value Object `Money` e o uso de métodos de extensão.

## Principais conceitos abordados

| Conceito                                   | Como foi abordado no projeto                                        |
|--------------------------------------------|--------------------------------------------------------------------|
| **Nullability / NRT**                      | O projeto habilita `Nullable` no arquivo `.csproj`, e propriedades/campos usam tipos não anuláveis sempre que possível. Guard clauses (`ArgumentNullException`) protegem contra referências nulas. |
| **Associações (0..1, 1..1, composição)**  | `OrderItem` possui obrigatoriamente um `Product` (1..1) e imutabilidade; `Order` compõe múltiplos `OrderItem` (1..n). |
| **Taxonomia de exceções**                  | `ArgumentOutOfRangeException` é usada para argumentos inválidos (ex.: estoque negativo); `DomainException` captura regras de domínio (ex.: estoque insuficiente). |
| **Métodos estáticos e extensões**         | `MathExtensions` demonstra métodos de extensão (`Square`, `Clamp`); a classe é estática e todos os métodos são puros (thread‑safe). |
| **Value Object**                           | `Money` é um objeto de valor imutável com validação de moeda e operações seguras. |
| **Refatoração e Boas Práticas**           | O código separa responsabilidades em namespaces/folders, usa `readonly` e propriedades imutáveis sempre que possível e evita duplicidade de código. |

## Executando o projeto

Este repositório contém apenas o código‑fonte. Para compilar e executar, é necessário ter o SDK do [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) ou superior instalado:

```bash
dotnet restore
dotnet run --project pratica-poo-csharp/CSharpPOOPractical.csproj
```

O output esperado demonstra operações básicas, tratamento de exceções e o uso de métodos de extensão.

## Como contribuir

* Fork este repositório e abra um *pull request* com suas melhorias.
* Sinta‑se à vontade para propor novos desafios ou refatorações.

---

Este projeto foi construído para fins educacionais e prática de conceitos de POO com C#. Se houver dúvidas ou sugestões, abra uma issue.
