# Murk.DesignPatterns
Cross cutting concern NuGets for quick implementation of design patterns.

### NuGets list

|                NuGet names                  | Version | Estate |
|---------------------------------------------|:-------:|:------:|
| Murk.DesignPatterns                         | 0.0.2   |  Beta  |
| Murk.DesignPatterns.BaseClasses             | 0.0.2   |  Beta  |
| Murk.DesignPatterns.Interfaces              | 0.0.2   |  Beta  |

### Breaking changes

From version `0.0.1` to `0.0.2` The command design pattern have several changes to follow SOLID principles and reduce code duplication.

Previously the `ICommand` interface implemented `System.Command`, also, each interface had the full ICommand implementation, 

```csharp
    public interface ICommandReversible : ICommand
    {
        void Reverse();
    }
```

now it only has the specific interface contract.

```csharp
    public interface IReversible
    {
        void Reverse();
    }
```

### Abalible design patterns

|           Design pattern names              |  Estate      |
|---------------------------------------------|:------------:|
| Command                                     | Included     |
| Factory                                     | Next release |
| Factory method                              | Next release |
| Repository                                  | Planed       |
| Unit of work                                | Planed       |
| Repository                                  | Planed       |
| Strategy                                    | Planed       |
| Singleton                                   | Planed       |
| Adapter / Wrapper / Traslator               | Planed       |
| Bridge / Mediator                           | Planed       |
| Composite                                   | Planed       |

And More to come in the future.

## Usage

For usage information read the [wiki!](https://github.com/Jerajo/Murk.DesignPatterns/wiki).

## Contributing

If you are interested in contributting, you can read the [CONTRIBUTING.md](https://github.com/Jerajo/Murk.DesignPatterns/wiki/Not-yet) file for more information.
