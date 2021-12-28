module puzzle9

open System

let makeHorizontalEntries start stop vert =
    if start < stop then
        let range = [start .. stop]
        let listOfPositions = range |> List.map (fun x -> (x, vert))
        listOfPositions
    else
        let range = [start .. -1 .. stop]
        let listOfPositions = range |> List.map (fun x -> (x, vert))
        listOfPositions

let makeVerticalEntries start stop horiz =
    if start < stop then 
        let range = [start .. stop]
        let listOfPositions = range |> List.map (fun y -> (horiz, y))
        listOfPositions
    else
        let range = [start .. -1 .. stop]
        let listOfPositions = range |> List.map (fun y -> (horiz, y))
        listOfPositions

let makeDiagonalEntries startX startY endX endY =
    if startX < endX then 
        if startY < endY then
            let xRange = [startX .. endX]
            let yRange = [startY .. endY]
            let listOfPositions = List.zip xRange yRange
            listOfPositions
        else
            let xRange = [startX .. endX]
            let yRange = [startY .. -1 .. endY]
            let listOfPositions = List.zip xRange yRange
            listOfPositions
    else
        if startY < endY then
            let xRange = [startX .. -1 .. endX]
            let yRange = [startY .. endY]
            let listOfPositions = List.zip xRange yRange
            listOfPositions
        else
            let xRange = [startX .. -1 .. endX]
            let yRange = [startY .. -1 .. endY]
            let listOfPositions = List.zip xRange yRange
            listOfPositions

let createTupleFromArray (stringArray : string[]) =
    let listOfStringArrays = stringArray |> Array.map (fun x -> x.Split(",")  |> Array.map System.Int32.Parse) |> Array.toList
    let verticalData = listOfStringArrays |> List.filter (fun x -> x.[0] = x.[2])
    let horizontalData = listOfStringArrays |> List.filter (fun x -> x.[1] = x.[3])
    let diagonalData = listOfStringArrays |> List.filter (fun x -> x.[0] <> x.[2] && x.[1] <> x.[3])
    (verticalData, horizontalData, diagonalData)


let processInputData (data: string[]) =
    let (verticalData, horizontalData, diagonalData) = data |> Array.map (fun x -> x.Replace(" -> ", ",") )|>  createTupleFromArray
    (verticalData, horizontalData, diagonalData)

let puz9 =
    let data = IO.File.ReadAllLines @"C:\adventofcode\05\input.txt"
    let (verticalData, horizontalData, diagonalData) = processInputData data
    let verticalEntries = verticalData |> List.map (fun x -> makeVerticalEntries x.[1] x.[3] x.[0]) |> List.concat
    let horizontalEntries = horizontalData |> List.map (fun x -> makeHorizontalEntries x.[0] x.[2] x.[1]) |> List.concat
    let diagonalEntries = diagonalData|> List.map (fun x -> makeDiagonalEntries x.[0] x.[1] x.[2] x.[3])  |> List.concat  
    let counts =   horizontalEntries @ verticalEntries |> List.sort |> List.countBy id
    let greaterThanOne = counts |> List.filter (fun x -> snd(x) > 1)
    printfn "Puzzle 9:  There are %d positions with more than 1" greaterThanOne.Length
    let allCounts = horizontalEntries @ verticalEntries @ diagonalEntries |> List.sort |> List.countBy id
    let allGreaterThanOne = allCounts |> List.filter (fun x -> snd(x) > 1)
    printfn "Puzzle 10: There are %d positions with more than 1" allGreaterThanOne.Length
    0

