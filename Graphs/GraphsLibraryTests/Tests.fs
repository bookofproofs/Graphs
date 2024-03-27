namespace GraphsLibraryTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open GraphsLibrary.GraphBasics


[<TestClass>]
type TestGraphBasics () =

    member this.DirectedGraph1() = 
        let g = DiGraph()
        let v1 = { Value = "a" }
        let v2 = { Value = 1 }
        let v3 = { Value = 2.0 }
        g.AddVertex(v1) |> ignore
        g.AddVertex(v2) |> ignore
        g.AddVertex(v3) |> ignore
        g.AddArrow({Start = v1; End = v2}) |> ignore
        g.AddArrow({Start = v1; End = v1}) |> ignore
        g.AddArrow({Start = v3; End = v2}) |> ignore
        g
        

    [<TestMethod>]
    member this.TestDigraph1Vertices () =
        let dig = this.DirectedGraph1()
        Assert.AreEqual<int>(3, dig.Vertices.Count);

    [<TestMethod>]
    member this.TestDigraph1Arrows () =
        let dig = this.DirectedGraph1()
        Assert.AreEqual<int>(3, dig.Arrows.Count)

    [<TestMethod>]
    member this.TestDigraph1ToString () =
        let dig = this.DirectedGraph1()
        let expected = """"a" -> 1:"a" -> "a":2.0 -> 1"""
        Assert.AreEqual<string>(expected, dig.ToString)

    [<TestMethod>]
    member this.TestDigraph1RemoveVertex () =
        let dig = this.DirectedGraph1()
        dig.RemoveVertex({Value = "a"}) |> ignore
        let expected = """2.0 -> 1"""
        Assert.AreEqual<string>(expected, dig.ToString)

    [<TestMethod>]
    member this.TestDigraph1RemoveArrow () =
        let dig = this.DirectedGraph1()
        let v = {Value = "a"}
        dig.RemoveArrow({Start = v; End = v}) |> ignore
        let expected = """"a" -> 1:2.0 -> 1"""
        Assert.AreEqual<string>(expected, dig.ToString)

    [<TestMethod>]
    member this.TestDigraph1AddNewVertex () =
        let dig = this.DirectedGraph1()
        Assert.IsTrue(dig.AddVertex({Value = "b"}))
        Assert.AreEqual<string>(""""b":"a" -> 1:"a" -> "a":2.0 -> 1""", dig.ToString);

    [<TestMethod>]
    member this.TestDigraph1AddExistingVertex () =
        let dig = this.DirectedGraph1()
        Assert.IsFalse(dig.AddVertex({Value = "a"}))
        Assert.AreEqual<string>(""""a" -> 1:"a" -> "a":2.0 -> 1""", dig.ToString)

    [<TestMethod>]
    member this.TestDigraph1AddNewArrow () =
        let dig = this.DirectedGraph1()
        let v = {Value = "a"}
        let w = {Value = 3}
        Assert.IsTrue(dig.AddArrow({Start = v; End = w}))
        Assert.AreEqual<string>(""""a" -> 1:"a" -> "a":2.0 -> 1:"a" -> 3""", dig.ToString)

    [<TestMethod>]
    member this.TestDigraph1AddExistingArrow () =
        let dig = this.DirectedGraph1()
        let v = {Value = "a"}
        let w = {Value = "a"}
        Assert.IsFalse(dig.AddArrow({Start = v; End = w}))
        Assert.AreEqual<string>(""""a" -> 1:"a" -> "a":2.0 -> 1""", dig.ToString);
