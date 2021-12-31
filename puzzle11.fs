module puzzle11

open System

let rec runIteration (iterations: int) (populationCounters : int[]) (last8Counts: int list) =
    let firstStep = populationCounters |> Array.map (fun x -> x - 1)
    let justMinusOnes = firstStep |> Array.filter (fun x -> x = -1)
    let minusOneCount = justMinusOnes.Length
    let secondStep = firstStep |> Array.map(fun x -> if x = -1 then 6 else x)
    let newOnes = Array.create minusOneCount 8
    let updatedPopulation = Array.append secondStep newOnes
    let lengthOfPopulation = [ updatedPopulation |> Array.length ]
    let addedLatestCount =  last8Counts |> List.append lengthOfPopulation
    let nextIteration = iterations - 1
    if nextIteration > 0 then
        runIteration nextIteration updatedPopulation addedLatestCount
    else
        addedLatestCount

let accumulateCounts (iterations: int) =
    let arrayOfOne = [|1|]
    let initialCounts = [1]
    let allCounts = runIteration iterations arrayOfOne initialCounts
    allCounts
    
let puz11 =
    let data = IO.File.ReadAllText @"C:\adventofcode\06\input.txt"
    let initialArray = data.Split(",") |> Array.map System.Int32.Parse
    let orderedArray = initialArray |> Array.countBy id
    let countsFor80Days = accumulateCounts 80
    let length80 = orderedArray |> Array.sumBy (fun (x,y) -> countsFor80Days.[x-1] * y)
    printfn "Puzzle 11: Number of lanternfish = %d" length80
    0