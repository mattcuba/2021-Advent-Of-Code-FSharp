module puzzle2

open System

let puz2 =
    let lines = IO.File.ReadLines @"C:\adventofcode\01\input.txt"
    let values = lines |> Seq.map System.Int32.Parse
    let window3 =  Seq.windowed 3 values |> Seq.map (fun[|x;y;z|] -> x + y + z) |> Seq.pairwise |> Seq.toList
    let largerThan = window3 |> List.filter (fun (x,y) -> x < y)
    printfn "Puzzle 2:  Windowed number of increases is  %d" largerThan.Length