﻿Public Class Form4
    Private Access As New DBControl
    Dim Start_Date As Date
    Dim End_Date As Date


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Not making username Editable
        Form2.USERNAME.Text = Form3.Label1.Text
        Form2.USERNAME.Enabled = False
        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Username='" & Form3.Label1.Text & "'")
        'if the faculty is logged in (Includes the case of HOD
        If Access.RecordCount > 0 Then

            Form2.FIRST_NAME.Text = Access.DBDT.Rows(0).Item("First_Name")
            Form2.LAST_NAME.Text = Access.DBDT.Rows(0).Item("Last_Name")
            'FOR Faculties which are not HOD
            If Access.DBDT.Rows(0).Item("Designation") <> "HOD" Then
                Form2.Faculty_Checkbox.Checked = True
                Form2.Student_Checkbox.Enabled = False
                Form2.HOD_CheckBox.Enabled = False

                Dim index2 As Integer = Form2.DEPARTMENT_FAC.FindString(Access.DBDT.Rows(0).Item("Department"))
                Form2.DEPARTMENT_FAC.SelectedIndex = index2

                Dim index As Integer = Form2.DESIGNATION.FindString(Access.DBDT.Rows(0).Item("Designation"))
                Form2.DESIGNATION.SelectedIndex = index

                Form2.PASSWORD.Text = Access.DBDT.Rows(0).Item("Password")
                Form2.PASSWORD.Enabled = False
                Form2.PASSWORD.PasswordChar = "*"
                Form2.DEPARTMENT_FAC.Visible = True
                Form2.DESIGNATION.Visible = True

                'For The HOD
            Else
                Form2.DEPARTMENT_FAC.SelectedText = Access.DBDT.Rows(0).Item("Department")
                Form2.PASSWORD.Text = Access.DBDT.Rows(0).Item("Password")
                Form2.PASSWORD.Enabled = False
                Form2.PASSWORD.PasswordChar = "*"
                Form2.HOD_CheckBox.Checked = True
                Form2.Student_Checkbox.Enabled = False
                Form2.Faculty_Checkbox.Enabled = False
                Form2.DEPARTMENT_FAC.Visible = True
            End If

            'If The student is logged in
        Else
            Access.AddParam("@user2", Form3.Label1.Text)
            Access.ExecQuery("SELECT * FROM Student_DB WHERE Username=@user2")
            If Access.RecordCount > 0 Then
                'Getting all the old Deatails and filling into the EDIT FORM
                Form2.FIRST_NAME.Text = Access.DBDT.Rows(0).Item("First_name")
                Form2.LAST_NAME.Text = Access.DBDT.Rows(0).Item("Last_name")
                Form2.ROLL_NO.Text = Access.DBDT.Rows(0).Item("Roll_no")
                Form2.YEAR.Text = Access.DBDT.Rows(0).Item("Year_of_joining")

                Dim index1 As Integer = Form2.PROGRAMME.FindString(Access.DBDT.Rows(0).Item("Programme"))
                Dim index2 As Integer = Form2.TA_SUPERVISER.FindString(Access.DBDT.Rows(0).Item("TA_Superviser"))
                Dim index3 As Integer = Form2.GUIDE.FindString(Access.DBDT.Rows(0).Item("Guide"))
                Dim index4 As Integer = Form2.DEPARTMENT.FindString(Access.DBDT.Rows(0).Item("Department"))
                Form2.PROGRAMME.SelectedIndex = index1
                Form2.TA_SUPERVISER.SelectedIndex = index2
                Form2.GUIDE.SelectedIndex = index3
                Form2.DEPARTMENT.SelectedIndex = index4

                Form2.PASSWORD.Text = Access.DBDT.Rows(0).Item("Password")
                Form2.PASSWORD.Enabled = False
                Form2.PASSWORD.PasswordChar = "*"
                Form2.Student_Checkbox.Checked = True
                Form2.Faculty_Checkbox.Enabled = False
                Form2.HOD_CheckBox.Enabled = False

            End If

        End If

        'Opening the EDIT FORM
        Me.Close()
        Form2.Show()
    End Sub

    Private Sub back_Button_Click(sender As Object, e As EventArgs) Handles back_Button.Click
        Me.Hide()
        Form3.Show()
    End Sub

End Class