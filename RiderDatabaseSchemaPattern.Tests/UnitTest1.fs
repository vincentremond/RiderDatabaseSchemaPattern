module RiderDatabaseSchemaPattern.Tests

open System.Threading.Tasks
open NUnit.Framework
open VerifyNUnit
open VerifyTests

let verify (r: obj) : Task =
    let settingsTask = Verifier.Verify r
    let task = SettingsTask.op_Implicit settingsTask
    task

[<Test>]
let Test1 () =
    let result = PatternBuilder.build ()
    verify result
