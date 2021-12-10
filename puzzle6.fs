module puzzle6

open System

let returnMajorityList (listZero: int[] list) (listOne: int[] list) =
    let returnZero = listZero.Length > listOne.Length
    let returnList = if returnZero then listZero else listOne 
    returnList
        
let returnMinorityList (listZero: int[] list) (listOne: int[] list) =
    let returnOne = listOne.Length < listZero.Length
    let returnList = if returnOne then listOne else listZero 
    returnList

let rec filterList (theList: int[] list) theIndex isMajority =
    if theIndex >= theList.Head.Length then theList
    elif theList.Length = 1 then theList
    else
        let zeroList = theList |> List.filter (fun x -> x[theIndex] = 0)
        let oneList = theList |> List.filter (fun x -> x[theIndex] = 1)
        let returnList = if isMajority then returnMajorityList zeroList oneList else returnMinorityList zeroList oneList
        let nextIndex = theIndex + 1
        filterList returnList nextIndex isMajority

let stringToIntArray input =
    let converted = input |> Array.ofSeq |> Array.map (fun x -> (int) x - 48)
    converted;

let binaryArrayToDecimal s = s |> Array.mapi (fun i j -> j * (pown 2 i)) |> Array.sum

let puz6 =
    let lines = IO.File.ReadLines @"C:\adventofcode\03\input.txt"
    let values = lines |> Seq.map stringToIntArray |> Seq.toList
    let firstList = filterList values 0 true
    let secondList = filterList values 0 false
    let reversedFirst = firstList.Head |> Array.rev
    let reversedSecond = secondList.Head |> Array.rev
    let decimalFirst = binaryArrayToDecimal reversedFirst
    let decimalSecond = binaryArrayToDecimal reversedSecond
    let product = decimalFirst * decimalSecond
    printfn "Puzzle 6:  The life support raiting is %d" product 



