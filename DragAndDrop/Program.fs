
open System
open Elmish.WPF
open DragAndDrop.Views

open System.Printing
open Elmish


type Printer = {
    fullname: string             // fullname includes the network location of the printer
    comment:string
    defaultPrintTicket: PrintTicket
    description: string
    isInError: bool
    isOffline: bool
}

type Model = {
    Printers: Printer list
}


type Msg =
    | RefreshMsg
   

let GetPrinters = 
    let LocalPrintServer = new PrintServer()
    let printQueues = LocalPrintServer.GetPrintQueues [|EnumeratedPrintQueueTypes.Local; EnumeratedPrintQueueTypes.Connections|]
    let printerList =
        printQueues
            |> Seq.cast<PrintQueue>
            |> Seq.toList
    printerList


let init () = {
   Printers = []
}


let update msg m   = 
    match msg with
        | RefreshMsg ->     let getprinters = 
                                GetPrinters |> List.map (fun pq -> {fullname = pq.FullName; comment = pq.Comment; defaultPrintTicket= pq.DefaultPrintTicket; 
                                description= pq.Description; isInError=pq.IsInError; isOffline=pq.IsOffline  } ) 

                            {m with Printers = getprinters }


let bindings () : Binding<Model,Msg> list = [
    "Printers"         |> Binding.oneWay(fun m -> m.Printers)
    "RefreshCommand"   |> Binding.cmd RefreshMsg
   ]


[<EntryPoint; STAThread>]
let main argv =
  Program.mkSimpleWpf init update bindings
  |> Program.runWindowWithConfig
    { ElmConfig.Default with LogConsole = true }   
    (MainWindow())