---
title:
  fr: "Les tests d'architecture avec ArchUnitNET"
  en: "Architecture tests with ArchUnitNET"
date: 2026-02-23
excerpt:
  fr: "Comment garantir la qualité et la cohérence de votre architecture logicielle grâce aux tests automatisés."
  en: "How to guarantee the quality and consistency of your software architecture through automated tests."
cover: ../../assets/img/cover.svg
tags:
  - dotnet
  - architecture
  - tests
  - ArchUnitNET
---

Avez-vous déjà travaillé sur un projet où, petit à petit, l'architecture s'est dégradée ? Un développeur pressé qui ajoute une dépendance "temporaire" entre deux couches, un autre qui nomme sa classe n'importe comment... Et un beau jour, vous vous retrouvez avec un beau bazar qu'on nommera "du legacy".

C'est exactement ce que les tests d'architecture permettent d'éviter.

## Le problème : l'érosion architecturale ##

Quand on démarre un projet, on définit généralement une architecture claire : Clean Architecture, Hexagonal, Onion... On trace des schémas, on explique les règles aux équipes. Mais avec le temps, ces règles s'oublient. Les nouveaux arrivants ne les connaissent pas forcément, et même les anciens peuvent faire des erreurs sous la pression des deadlines.

Le résultat ? Une architecture qui se dégrade progressivement. C'est ce qu'on appelle l'**érosion architecturale** ou encore, selon Wikipédia, la **dégradation logicielle** en bon français. Et une fois que le mal est fait, c'est très coûteux à corriger.

## La solution : automatiser la validation ##

Et si on pouvait valider automatiquement que notre code respecte les règles architecturales définies ? C'est exactement ce que permettent les tests d'architecture.

L'idée est simple : écrire des tests qui vérifient que :
- Les dépendances entre couches sont correctes
- Les conventions de nommage sont respectées
- Les classes sont placées dans les bons namespaces
- Et bien plus encore...

Ces tests s'exécutent en local mais surtout dans votre CI/CD, comme n'importe quel autre test. Si quelqu'un viole une règle architecturale, le build échoue. Impossible de merger du code qui casse l'architecture !

## ArchUnitNET : l'outil parfait pour .NET ##

Pour les projets .NET, [ArchUnitNET](https://github.com/TNG/ArchUnitNET) est la référence. C'est un port de la célèbre librairie Java ArchUnit. Elle s'intègre parfaitement avec xUnit (ou NUnit) et propose une API très lisible.

### Installation ###

Commencez par créer un projet de test dédié et ajoutez le package ArchUnitNET :

```PS
Install-Package TngTech.ArchUnitNET.xUnit
```

Si vous n'utilisez pas xUnit, d'autres packages existent :

```PS
Install-Package TngTech.ArchUnitNET
Install-Package TngTech.ArchUnitNET.NUnit
Install-Package TngTech.ArchUnitNET.MSTestV2
```

N'oubliez pas d'ajouter des références vers tous les projets que vous souhaitez analyser.

### Charger l'architecture ###

La première étape consiste à charger les assemblies que vous voulez analyser. Pour faire ça, il suffit de référencer un type présent dans chacune des assemblies :

```csharp
private static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(
        typeof(Domain.Entities.MonEntity).Assembly,
        typeof(Application.Services.MonService).Assembly,
        typeof(Infrastructure.Persistence.MonRepository).Assembly
    .Build();
```

Cet objet `Architecture` sera ensuite utilisée par tous vos tests.

## Exemples concrets ##

Passons aux choses sérieuses avec des exemples.

### 1. Valider les dépendances entre couches ###

En Clean Architecture, le Domain ne doit dépendre de rien d'autre. Voici comment le vérifier :

```csharp
private static readonly IObjectProvider<IType> DomainLayer =
    Types().That().ResideInNamespace("MonProjet.Domain.*", useRegularExpressions: true)
        .As("Domain Layer");

private static readonly IObjectProvider<IType> ApplicationLayer =
    Types().That().ResideInNamespace("MonProjet.Application.*", useRegularExpressions: true)
        .As("Application Layer");

[Fact]
public void DomainLayer_ShouldNotDependOn_ApplicationLayer()
{
    var rule = Types()
        .That()
        .Are(DomainLayer)
        .Should()
        .NotDependOnAny(ApplicationLayer)
        .Because("Domain layer should not depend on Application layer (Clean Architecture)");

    rule.Check(Architecture);
}
```

Ce test échouera si quelqu'un ajoute un `using` vers l'Application depuis le Domain. Simple et efficace !

Vous pouvez créer des tests similaires pour toutes les règles de dépendance :
- Le Domain ne dépend de rien
- L'Application ne dépend que du Domain
- L'Infrastructure peut dépendre du Domain et de l'Application

### 2. Conventions de nommage ###

Les conventions de nommage sont souvent négligées, mais elles facilitent énormément la navigation dans le code. Voici comment les renforcer :

```csharp
[Fact]
public void Interfaces_ShouldStartWith_I()
{
    var rule = Interfaces()
        .Should()
        .HaveNameStartingWith("I")
        .Because("Interface names should start with 'I' prefix by convention");

    rule.Check(Architecture);
}

[Fact]
public void ServiceClasses_ShouldEndWith_Service()
{
    var rule = Classes()
        .That()
        .ResideInNamespace(".*Services.*", useRegularExpressions: true)
        .Should()
        .HaveNameEndingWith("Service")
        .Because("Service class names should end with 'Service'");

    rule.Check(Architecture);
}
```

### 3. Placement des classes ###

Où placer les interfaces de Repository ? Dans le Domain bien sûr, pour respecter l'inversion de dépendance :

```csharp
[Fact]
public void RepositoryInterfaces_ShouldResideInDomainOrRepositoriesNamespace()
{
    var rule = Interfaces()
        .That()
        .HaveNameEndingWith("Repository")
        .Should()
        .ResideInNamespace(".*Repositories.*", useRegularExpressions: true)
        .Because("Repository interfaces should be defined in a Repositories namespace");

    rule.Check(Architecture);
}

[Fact]
public void RepositoryImplementations_ShouldBeInInfrastructureLayer()
{
    var rule = Classes()
        .That()
        .HaveNameEndingWith("Repository")
        .And()
        .AreNotAbstract()
        .Should()
        .ResideInNamespace(".*Infrastructure.*", useRegularExpressions: true)
        .Because("Repository implementations should be in Infrastructure layer");

    rule.Check(Architecture);
}
```

## Organisation des tests ##

Je recommande d'organiser vos tests d'architecture en plusieurs fichiers selon leur préoccupation :

- **LayerDependencyTests.cs** : règles de dépendances entre couches
- **NamingConventionTests.cs** : conventions de nommage
- **StructuralTests.cs** : placement des classes, visibilité, etc.

Cette organisation facilite la maintenance et permet à chacun de comprendre rapidement quelles règles sont en place.

## Bonnes pratiques ##

Quelques conseils tirés de mon expérience :

### Commencez petit ###
N'essayez pas de tout couvrir dès le départ. Commencez par les règles les plus importantes (dépendances entre couches) et ajoutez progressivement.

### Utilisez `.Because()` ###
Expliquez toujours pourquoi une règle existe. Ça aide à comprendre l'intention et à ne pas simplement contourner le test.

### Utilisez les regex pour les namespaces ###
Les namespaces exacts sont fragiles. Préférez les patterns regex comme `".*Domain.*"` pour être plus flexible.

### Exécutez dans la CI ###
Ces tests n'ont de valeur que s'ils sont bloquants. Intégrez-les à votre pipeline CI/CD.

### Documentez les exceptions ###
Si vous devez ignorer certains cas, documentez pourquoi. Utilisez les mécanismes d'exclusion d'ArchUnitNET plutôt que de supprimer le test.

## Conclusion ##

Les tests d'architecture sont un investissement qui paie sur le long terme. Ils :
- **Documentent** votre architecture
- **Protègent** de l'érosion architecturale
- **Éduquent** les nouveaux membres de l'équipe
- **Automatisent** des vérifications autrement manuelles

Le coût de mise en place est faible comparé aux bénéfices. Sur un projet existant, vous pouvez même utiliser ces tests pour identifier les violations actuelles avant de les corriger progressivement.

Alors, qu'attendez-vous pour ajouter des tests d'architecture à votre projet ?

## Ressources ##

- [ArchUnitNET sur GitHub](https://github.com/TNG/ArchUnitNET)
- [Clean Architecture de Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

<!-- lang:en -->

Have you ever worked on a project where the architecture gradually degraded? A developer in a hurry who adds a "temporary" dependency between two layers, another who names their class carelessly... And one day, you end up with a big mess that we'll call "legacy".

That's exactly what architecture tests help prevent.

## The Problem: Architectural Erosion ##

When starting a project, we generally define a clear architecture: Clean Architecture, Hexagonal, Onion... We draw diagrams, explain the rules to the teams. But over time, these rules are forgotten. New team members may not know them, and even experienced ones can make mistakes under deadline pressure.

The result? An architecture that degrades progressively. This is what we call **software erosion** or, according to Wikipedia, **software rot**. And once the damage is done, it's very costly to fix.

## The Solution: Automating Validation ##

What if we could automatically validate that our code follows the defined architectural rules? That's exactly what architecture tests allow.

The idea is simple: write tests that verify that:
- Dependencies between layers are correct
- Naming conventions are respected
- Classes are placed in the right namespaces
- And much more...

These tests run locally but especially in your CI/CD pipeline, like any other test. If someone violates an architectural rule, the build fails. It becomes impossible to merge code that breaks the architecture!

## ArchUnitNET: The Perfect Tool for .NET ##

For .NET projects, [ArchUnitNET](https://github.com/TNG/ArchUnitNET) is the reference. It's a port of the famous Java ArchUnit library. It integrates perfectly with xUnit (or NUnit) and offers a very readable API.

### Installation ###

Start by creating a dedicated test project and add the ArchUnitNET package:

```PS
Install-Package TngTech.ArchUnitNET.xUnit
```

If you're not using xUnit, other packages are available:

```PS
Install-Package TngTech.ArchUnitNET
Install-Package TngTech.ArchUnitNET.NUnit
Install-Package TngTech.ArchUnitNET.MSTestV2
```

Don't forget to add references to all the projects you want to analyze.

### Loading the Architecture ###

The first step is to load the assemblies you want to analyze. To do this, you only need to reference a type present in each assembly:

```csharp
private static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(
        typeof(Domain.Entities.MyEntity).Assembly,
        typeof(Application.Services.MyService).Assembly,
        typeof(Infrastructure.Persistence.MyRepository).Assembly
    .Build();
```

This `Architecture` object will then be used by all your tests.

## Concrete Examples ##

Let's get to the point with some examples.

### 1. Validating Dependencies Between Layers ###

In Clean Architecture, the Domain should not depend on anything else. Here's how to verify it:

```csharp
private static readonly IObjectProvider<IType> DomainLayer =
    Types().That().ResideInNamespace("MyProjet.Domain.*", useRegularExpressions: true)
        .As("Domain Layer");

private static readonly IObjectProvider<IType> ApplicationLayer =
    Types().That().ResideInNamespace("MyProjet.Application.*", useRegularExpressions: true)
        .As("Application Layer");

[Fact]
public void DomainLayer_ShouldNotDependOn_ApplicationLayer()
{
    var rule = Types()
        .That()
        .Are(DomainLayer)
        .Should()
        .NotDependOnAny(ApplicationLayer)
        .Because("Domain layer should not depend on Application layer (Clean Architecture)");

    rule.Check(Architecture);
}
```

This test will fail if someone adds a `using` pointing to the Application layer from the Domain layer. Simple and effective!

You can create similar tests for all dependency rules:
- Domain depends on nothing
- Application depends only on Domain
- Infrastructure can depend on Domain and Application

### 2. Naming Conventions ###

Naming conventions are often overlooked, but they greatly ease navigation in code. Here's how to enforce them:

```csharp
[Fact]
public void Interfaces_ShouldStartWith_I()
{
    var rule = Interfaces()
        .Should()
        .HaveNameStartingWith("I")
        .Because("Interface names should start with 'I' prefix by convention");

    rule.Check(Architecture);
}

[Fact]
public void ServiceClasses_ShouldEndWith_Service()
{
    var rule = Classes()
        .That()
        .ResideInNamespace(".*Services.*", useRegularExpressions: true)
        .Should()
        .HaveNameEndingWith("Service")
        .Because("Service class names should end with 'Service'");

    rule.Check(Architecture);
}
```

### 3. Class Placement ###

Where should Repository interfaces be placed? In the Domain of course, to respect dependency inversion:

```csharp
[Fact]
public void RepositoryInterfaces_ShouldResideInDomainOrRepositoriesNamespace()
{
    var rule = Interfaces()
        .That()
        .HaveNameEndingWith("Repository")
        .Should()
        .ResideInNamespace(".*Repositories.*", useRegularExpressions: true)
        .Because("Repository interfaces should be defined in a Repositories namespace");

    rule.Check(Architecture);
}

[Fact]
public void RepositoryImplementations_ShouldBeInInfrastructureLayer()
{
    var rule = Classes()
        .That()
        .HaveNameEndingWith("Repository")
        .And()
        .AreNotAbstract()
        .Should()
        .ResideInNamespace(".*Infrastructure.*", useRegularExpressions: true)
        .Because("Repository implementations should be in Infrastructure layer");

    rule.Check(Architecture);
}
```

## Organizing Tests ##

I recommend organizing your architecture tests into multiple files based on their concern:

- **LayerDependencyTests.cs**: layer dependency rules
- **NamingConventionTests.cs**: naming conventions
- **StructuralTests.cs**: class placement, visibility, etc.

This organization makes maintenance easier and lets anyone quickly understand what rules are in place.

## Best Practices ##

Some tips from my experience:

### Start Small ###
Don't try to cover everything from the start. Begin with the most important rules (layer dependencies) and add progressively.

### Use `.Because()` ###
Always explain why a rule exists. It helps understand the intention and prevents simply bypassing the test.

### Use Regex for Namespaces ###
Exact namespaces are fragile. Prefer regex patterns like `".*Domain.*"` for more flexibility.

### Run in CI ###
These tests only have value if they're blocking. Integrate them into your CI/CD pipeline.

### Document Exceptions ###
If you need to ignore certain cases, document why. Use ArchUnitNET's exclusion mechanisms rather than deleting the test.

## Conclusion ##

Architecture tests are an investment that pays off in the long run. They:
- **Document** your architecture
- **Protect** against architectural erosion
- **Educate** new team members
- **Automate** otherwise manual verifications

The setup cost is low compared to the benefits. On an existing project, you can even use these tests to identify current violations before progressively fixing them.

So, what are you waiting for to add architecture tests to your project?

## Resources ##

- [ArchUnitNET on GitHub](https://github.com/TNG/ArchUnitNET)
- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
