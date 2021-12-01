module puzzle1

open System

let puz1 =
    let lines = IO.File.ReadLines @"C:\adventofcode\01\input.txt"
    let values = lines |> Seq.map System.Int32.Parse|> Seq.pairwise |> Seq.toList
    let largerThan = values |> List.filter (fun (x,y) -> x < y)
    printfn "Puzzle 1:  Number of increases is %d" largerThan.Length