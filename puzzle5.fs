module puzzle5

open System

let reduceToZeroOrOne array =
    let reduced = array |> Array.sumBy (fun n -> if ( n = 0 ) then -1 else 1) |>  (fun n -> if (n < 0) then 0 else 1)
    reduced

let stringToIntArray input =
    let converted = input |> Array.ofSeq |> Array.map (fun x -> (int) x - 48)
    converted;;

let flipIt i = abs (i-1)

let binaryArrayToDecimal s = s |> Array.mapi (fun i j -> j * (pown 2 i)) |> Array.sum

let puz5 =
    // read in the strings
    let lines = IO.File.ReadLines @"C:\adventofcode\03\input.txt"
    // convert to an array of integer arrays
    let values = lines |> Seq.map stringToIntArray |> Seq.toArray
    // turn the very long array of short arrays into a very short array of long arrays
    let transposed = values |> Array.transpose
    // determine gamma and epsilon
    let gamma = transposed |> Array.map (fun x -> reduceToZeroOrOne x)
    let epsilon = gamma |> Array.map flipIt
    // reverse the arrays
    let reversedGamma = gamma |> Array.rev
    let reversedEpsilon = epsilon |> Array.rev
    // convert the binary arrays into decimal values
    let decimalGamma = binaryArrayToDecimal reversedGamma
    let decimalEpsilon = binaryArrayToDecimal reversedEpsilon 
    let product = decimalGamma * decimalEpsilon
    printfn "Puzzle 5:  The power consumption is %d" product
