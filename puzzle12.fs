module puzzle12

open System

let countNumberOf number inArray = 
    let (theCount: int64) = inArray |> Array.filter (fun x -> x = number) |> Array.length |> int64
    theCount

let createArrayOfCounters (initialCountersArray : int[]) =
    let counterIndexes = [|0..8|]
    let populatedCounters = counterIndexes |> Array.map (fun x -> countNumberOf x initialCountersArray)
    populatedCounters

let rec iterateArray (theArray: int64[]) (iterations: int) = 
    let numToProduce = theArray.[0]
    let counterIndexes = [|0..8|]
    let shiftedArray = counterIndexes |> Array.map (fun x -> if x <> 8 then theArray.[x+1] else 0)
    let resetArray = counterIndexes |> Array.map (fun x -> if x <> 6 then shiftedArray.[x] else shiftedArray.[x]+numToProduce)
    let addedArray = counterIndexes |> Array.map (fun x -> if x <> 8 then resetArray.[x] else numToProduce)
    let updatedIterations = iterations - 1
    if updatedIterations > 0 then
        iterateArray addedArray updatedIterations
    else
        let total = addedArray |> Array.sum
        total

let puz12 =
    let data = IO.File.ReadAllText @"C:\adventofcode\06\input.txt"
    let initialArray = data.Split(",") |> Array.map System.Int32.Parse |> Array.sort
    let arrayOfCounters = createArrayOfCounters initialArray 
    let total256 = iterateArray arrayOfCounters 256
    printfn "Puzzle 12: Number of lanternfish = %d" total256
