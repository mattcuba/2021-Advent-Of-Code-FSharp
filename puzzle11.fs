module puzzle11

open System

let rec runIteration (iterations: int) (inboundCounts : int[]) =
    let firstStep = inboundCounts |> Array.map (fun x -> x - 1)
    let justMinusOnes = firstStep |> Array.filter (fun x -> x = -1)
    let minusOneCount = justMinusOnes.Length
    let secondStep = firstStep |> Array.map(fun x -> if x = -1 then 6 else x)
    let newOnes = Array.create minusOneCount 8
    let updatedCounts = Array.append secondStep newOnes
    let nextIteration = iterations - 1
    if nextIteration > 0 then
        runIteration nextIteration updatedCounts
    else
        updatedCounts.Length

let accumulateCounts (groupedData: (int*int)[]) (iterations: int) =
    //let numberByGroup = groupedData |> Array.map (fun (x,y) -> (runIteration iterations (Array.create 1 x) ) * y)
    let numberByGroup = groupedData |> Array.map (fun (x,y) -> (runIteration iterations (Array.create 1 x) ), y)
    //let total = numberByGroup |> Array.sum
    //total
    numberByGroup
    
let puz11 =
    let data = IO.File.ReadAllText @"C:\adventofcode\06\input.txt"
    let initialArray = data.Split(",") |> Array.map System.Int32.Parse
    let orderedArray = initialArray |> Array.countBy id
    orderedArray |> Array.iter (fun x -> printfn "%A" x)
    let grouped80 = accumulateCounts orderedArray 80
    printfn "80 days:"
    grouped80 |> Array.iter (fun (x,y) -> printfn "%d  %d" x y)
    //printfn "Puzzle 11:  There are %d after 80 days" total80
    printfn "Now doing 256..."
    let grouped256 = accumulateCounts orderedArray 256
    printfn "256 days:"
    grouped256 |> Array.iter (fun (x,y) -> printfn "%d  %d" x y)
   //printfn "Puzzle 12:  there are %d after 256 days" total256
    //let finalCounts = runIteration 80 initialArray
    //printfn "Puzzle 11:  There are %d items" finalCounts.Length
    //let countAfter256Days = runIteration 256 initialArray
    //printfn "Puzzle 12:  There are %d items" countAfter256Days.Length

    0