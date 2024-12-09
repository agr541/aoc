module AoC2024

open System
open System.Linq

let quit () =
    printfn "bye!"
    exit 0

let menuItems = ModuleList.modules

let defaultOption = menuItems.Keys.TakeLast(1).Single().ToUpper()

let menuChoice () =
    printfn $"Enter choice [{defaultOption}]:"
    let choice = System.Console.ReadLine().ToUpper()
    if choice = "" then defaultOption else choice

let printMenu () =
    printfn "AOC 2024:"

    for key in Map.keys menuItems do
        printfn "%s" key

    printfn "Q to quit"
    printfn ""

let rec runMenuItem menuItem =
    if menuItem = "Q" then
        quit ()
    else if menuItems |> Map.containsKey menuItem then
        printfn $"running {menuItem}..."
        let results = menuItems.[menuItem] ()

        for result in results do
            printfn "%s" result
    else
        printfn $"invalid choice: {menuItem}"

let rec menu (first: bool) =
    printMenu ()

    if first then
        runMenuItem (defaultOption)
    else
        runMenuItem (menuChoice ())

    printfn ""
    menu (false)

menu (true)
