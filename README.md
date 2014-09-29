FodyReproduce
=============

Repo for reproducing a bug in Fody / FSharp tooling.

To reproduce the error:
- Open the solution in VS
- Build (this will install the NuGet packages)
- **Close and reopen VS** (or at least unload the F# project), otherwise the Fody plugin is not run
- Rebuild the solution in VS.
- Look the *Error List* window.

The expected behaviour is, that both C# and F# behave identically, which is apparently not the case at the moment.
