Imports System
Imports System.IO

Module Program
    Public Structure empleados
        Public ID As Integer
        Public NOMBRE As String
        Public DEP As Integer
        Public EDAD As Integer
        Public ANOSTR As Integer
    End Structure
    Public Structure usuarios
        Public IDEMP As Integer
        Public CONTRASEÑA As String
    End Structure
    Public Structure departamentos
        Public IDDEPAR As Integer
        Public NOMBREDEPAR As String
    End Structure

    Public empleado1 As New empleados With {.ID = 1, .NOMBRE = "PEDRO MARCOS", .DEP = 2, .EDAD = 56, .ANOSTR = 34}
    Public empleado2 As New empleados With {.ID = 2, .NOMBRE = "ANA ATUTXA", .DEP = 1, .EDAD = 23, .ANOSTR = 4}

    Public usuario1 As New usuarios With {.IDEMP = empleado1.ID, .CONTRASEÑA = "1234"}
    Public usuario2 As New usuarios With {.IDEMP = empleado2.ID, .CONTRASEÑA = "QWER"}

    Public departamento1 As New departamentos With {.IDDEPAR = 1, .NOMBREDEPAR = "Atención al cliente"}
    Public departamento2 As New departamentos With {.IDDEPAR = 2, .NOMBREDEPAR = "Logística"}
    Public departamento3 As New departamentos With {.IDDEPAR = 3, .NOMBREDEPAR = "Gerencia"}

    Public listEmpleados As New List(Of empleados) From {empleado1, empleado2}
    Public listUsuarios As New List(Of usuarios) From {usuario1, usuario2}
    Public listDepartamentos As New List(Of departamentos) From {departamento1, departamento2, departamento3}

    Public Function estaRegistradoUsuario(id As Integer)
        Dim registrado = False
        Dim i = 0

        While registrado = False And i < listUsuarios.Count
            If id = listUsuarios(i).IDEMP Then
                registrado = True
            Else
                i += 1
            End If
        End While

        Return registrado
    End Function

    Public Function estaRegistradoEmpleado(id As Integer)
        Dim registrado = False
        Dim i = 0

        While registrado = False And i < listEmpleados.Count
            If id = listEmpleados(i).ID Then
                registrado = True
            Else
                i += 1
            End If
        End While

        Return registrado
    End Function

    Public Function posicionUsuario(id As Integer)
        Dim encontrado = False
        Dim i = 0

        If estaRegistradoUsuario(id) Then

            While encontrado = False And i < listUsuarios.Count
                If id = listUsuarios(i).IDEMP Then
                    encontrado = True
                Else
                    i += 1
                End If
            End While
        End If

        Return i
    End Function

    Public Function posicionEmpleado(id As Integer)
        Dim encontrado = False
        Dim i = 0

        If estaRegistradoEmpleado(id) Then

            While encontrado = False And i < listEmpleados.Count
                If id = listEmpleados(i).ID Then
                    encontrado = True
                Else
                    i += 1
                End If
            End While
        End If

        Return i
    End Function

    Public Function calcSueldo(id As Integer)
        Dim sueldo = 0
        Dim base = 1500

        If estaRegistradoEmpleado(id) Then
            Dim pos = posicionEmpleado(id)
            Dim edad = listEmpleados(pos).EDAD

            If edad < 18 Then
                Console.WriteLine(edad + " No son suficientes años para trabajar.")
            ElseIf edad >= 18 And edad <= 50 Then
                sueldo = base + base * 0.05
            ElseIf edad > 50 And edad <= 60 Then
                sueldo = base + base * 0.1
            ElseIf edad > 60 Then
                sueldo = base + base * 0.15
            End If

        End If

        Return sueldo
    End Function

    Public Function calcVacaciones(id As Integer)
        Dim vacaciones = 0

        If estaRegistradoEmpleado(id) Then
            Dim i = posicionEmpleado(id)
            Dim anos = listEmpleados(i).ANOSTR
            Dim dep = listEmpleados(i).DEP

            If anos > 2 And anos < 7 Then
                vacaciones = 15
            ElseIf anos >= 7 Then
                If dep = 1 Then
                    vacaciones = 20
                ElseIf dep = 2 Then
                    vacaciones = 25
                ElseIf dep = 3 Then
                    vacaciones = 30
                End If
            End If
        End If

        Return vacaciones
    End Function

    Public Function crearFichero()
        Dim ruta As String = "F:\Dato\Registro.log"

        Dim fS As FileStream = File.Create(ruta)


        fS.Close()

        FileOpen(1, "F:\Dato\registroEmpleados.log", OpenMode.Output)

        ' Escribimos el encabezado
        PrintLine(1, "Fecha: " + Now + "\n\n")

        Dim titulo = ("| NOMBRE | EDAD | T. LABORADO | DÍAS VAC. | SALARIO (EUROS) |")

        PrintLine(1, titulo)

    End Function

    Public Function escribirEnFichero(linea As String)
        ' FileOpen(1, "C:\Users\Ainara\Documents\AA_EMPLEADOS\registroEmpleados.log", OpenMode.Output) ------------------------------------------------------------------------------------------------------------------------------------------------------------------
        PrintLine(1, linea)

    End Function

    Public Function escribirDatosEmpleado(id As Integer)
        'Dim id = 1
        Dim i = posicionEmpleado(id)

        Console.WriteLine("| NOMBRE | EDAD | T. LABORADO | DÍAS VAC. | SALARIO (EUROS) |")

        Dim linea = String.Concat("| ", listEmpleados(i).NOMBRE, " | ", listEmpleados(i).EDAD, " | ", listEmpleados(i).ANOSTR, " | ", calcVacaciones(id), " | ", calcSueldo(id), " |")

        Console.WriteLine(linea)
        escribirEnFichero(linea)

    End Function

    Public Sub crearUsuario()
        Dim id As Integer
        Dim continuar As Boolean
        Do
            Console.WriteLine("Introduzca su ID: ")
            id = Console.ReadLine
            If estaRegistradoUsuario(id) Then
                Console.WriteLine("Ya existe un usuario registrado con el ID: " + id)
                continuar = True
            Else
                continuar = False
            End If
        Loop While continuar = True

        ' Comprueba que las contraseñas coinciden
        Dim con1, con2 As String
        Do
            Console.WriteLine("Introduzca la contraseña: ")
            con1 = Console.ReadLine
            Console.WriteLine("Vuelva a introducir la contraseña: ")
            con2 = Console.ReadLine
            If con1 <> con2 Then
                Console.WriteLine("Las contraseñas introducidas no coinciden.")
            End If
        Loop Until con1 = con2
        Console.WriteLine("Introduzca el nombre completo: ")
        Dim nombre = Console.ReadLine

        Dim dep As Integer
        Do
            Console.WriteLine("Introduzca el ID del departamento: ")
            dep = Console.ReadLine
            Dim i = 0
            continuar = True
            Do
                If dep = listDepartamentos(i).IDDEPAR Then
                    continuar = False
                Else
                    i += 1
                End If
            Loop While continuar And i < listDepartamentos.Count
            If continuar = True Then
                Console.WriteLine("No existe el departamento introducido.")
            Else
                Console.WriteLine("Pertenece al departamento: " + listDepartamentos(i).NOMBREDEPAR)
            End If
        Loop While continuar

        Console.WriteLine("Introduzca la edad: ")
        Dim edad = Console.ReadLine
        Console.WriteLine("Introduzca los años trabajados: ")
        Dim anos = Console.ReadLine

        Dim nuevoEmpleado As New empleados With {.ID = id, .NOMBRE = nombre, .DEP = dep, .EDAD = edad, .ANOSTR = anos}
        Dim nuevoUser As New usuarios With {.IDEMP = id, .CONTRASEÑA = con1}

        listEmpleados.Add(nuevoEmpleado)
        listUsuarios.Add(nuevoUser)

        Console.WriteLine("Ha sido registrado correctamente")

    End Sub

    Public Function login()

        Dim continuar = True

        Do
            Console.WriteLine("Introduzca su ID de usuario: ")
            Dim user = Console.ReadLine

            If user = 0 Then
                Return False
            Else
                Console.WriteLine("Introduzca su contraseña: ")
                Dim con = Console.ReadLine

                Dim usuario As New usuarios With {.IDEMP = user, .CONTRASEÑA = con}

                If listUsuarios.IndexOf(usuario) = -1 Then
                    Console.WriteLine("Usuario o contraseña erróneos")
                    Console.WriteLine("Introduzca '1' para intentarlo de nuevo o '2' para iniciar el registro de un nuevo usuario: ")
                    Dim num = Console.ReadLine

                    If num = 2 Then
                        crearUsuario()
                    End If

                Else
                    escribirDatosEmpleado(user)
                    continuar = False
                End If
                Return True
            End If
        Loop While continuar = True
    End Function

    Sub Main(args As String())
        crearFichero()
        Dim continuar As Boolean
        Do
            continuar = login()
        Loop While continuar

        FileClose()
    End Sub
End Module
