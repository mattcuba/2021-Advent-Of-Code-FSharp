// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

[<EntryPoint>]
let main argv =
    let lines = IO.File.ReadLines @"C:\adventofcode\01\input.txt"
    let values = lines |> Seq.map System.Int32.Parse|> Seq.pairwise |> Seq.toList
    let largerThan = values |> List.filter (fun (x,y) -> x < y)
    printfn "Number of increases: %d" largerThan.Length

    0 // return an integer exit code