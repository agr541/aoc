module D202403

open System
open System.Diagnostics
open System.Linq
open System.Collections.Generic
open System.IO

let ProcessInput (input:string) =
    let mulOps = input.Split("mul(").Skip(1).ToArray()
    let mulOpsArgs = mulOps |> Array.map _.Split(")").Take(1).ToArray()

    let mulOpsArgsArr =
        mulOpsArgs
        |> Array.map (fun line ->
            line
            |> Array.map _.Split(",")
            |> Array.filter (fun argSplitted -> argSplitted.Length = 2))

    let mulOpsArgsArrValidNumbers =
        mulOpsArgsArr
        |> Array.map (fun line ->
            line
            |> Array.filter (fun argsArr ->
                argsArr.Length > 0
                && argsArr.Length <= 3
                && argsArr
                   |> Array.forall (fun arg -> arg.ToCharArray() |> Array.forall Char.IsNumber)))
        |> Array.concat

    let parsed =
        mulOpsArgsArrValidNumbers
        |> Array.map (fun arrArg -> arrArg |> Array.map Int32.Parse)

    let multiplied =
        parsed
        |> Array.filter (fun arr -> arr.Length = 2)
        |> Array.map (fun arr -> arr |> Array.reduce (*))

    let summed = multiplied |> Array.sum
    let answer = summed
    answer

let A (inputFile: string) =
    let input = File.ReadAllText(inputFile)
    ProcessInput input

let B (inputFile: string) =
    let input = File.ReadAllText(inputFile)

    let filteredInput =
        $"do(){input}".Split("do()")
        |> Array.map _.Split("don't()")
        |> Array.map _.Take(1)
        |> Array.map ( fun s-> String.concat "" s )
        |> String.concat ""
    
    ProcessInput filteredInput

let menuItemName = Path.GetFileName(__SOURCE_DIRECTORY__).TrimStart("0")
let inputs = Directory.EnumerateFiles(__SOURCE_DIRECTORY__, "*.txt")

let runOnInputs (func: string -> int, inputs: IEnumerable<string>, funcName: string) =

    inputs
    |> Seq.sortBy (fun input -> if input.Contains(funcName) then $"*{input}" else input)
    |> Seq.map (fun input ->
        let result = func input
        $"{Path.GetFileNameWithoutExtension(input)}:{result}")

let methods =
    Map.ofList
        [ ($"{menuItemName}A", (fun () -> runOnInputs (A, inputs, "A")))
          ($"{menuItemName}B", (fun () -> runOnInputs (B, inputs, "B"))) ]
    |> Map.toList
