﻿Public Class Form3
    Private Access As New DBControl
    Dim Start_Date As Date
    Dim End_Date As Date

    Private Sub Form3_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If Label1.Text = "ADMIN" Then
            Me.tabctrlMainTabs.TabPages.Remove(tabpgLeavesToApprove)
            Me.tabctrlMainTabs.TabPages.Remove(tabpgViewLeaves)
            Me.tabctrlMainTabs.TabPages.Remove(tabpgNotifications)
            Me.tabctrlMainTabs.TabPages.Remove(tabpgNewLeaves)
            Me.BALANCES.Visible = False
            Me.Edit.Visible = False

            Dim help As String = "PENDING"
            Access.ExecQuery("SELECT * FROM Student_DB WHERE Approved='" & help & "'")
            If Access.RecordCount > 0 Then
                For Each r As DataRow In Access.DBDT.Rows
                    Dim dum As String = r(0)
                    Dim dum2 As String = "STUDENT"
                    Dim v As New ListViewItem(dum)
                    v.SubItems.Add(r(5))
                    v.SubItems.Add(r(6))
                    v.SubItems.Add(r(4))
                    v.SubItems.Add(dum2)
                    ADMIN.Items.Add(v)
                Next
            End If

            Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Approved='" & help & "'")
            If Access.RecordCount > 0 Then
                For Each r As DataRow In Access.DBDT.Rows
                    Dim dum As String = r(0)
                    Dim dum2 As String = "FACULTY"
                    Dim v As New ListViewItem(dum)
                    v.SubItems.Add(r(2))
                    v.SubItems.Add(r(3))
                    v.SubItems.Add(r(4))
                    v.SubItems.Add(dum2)
                    ADMIN.Items.Add(v)
                Next
            End If
        Else
            Me.tabctrlMainTabs.TabPages.Remove(TabPage1)
        End If
        If Label4.Text <> "M.Tech/M.Sc" And Label4.Text <> "PhD" Then
            Me.tabctrlMainTabs.TabPages.Remove(tabpgLeavesToApprove)
        End If

        ' INITIALIZE VIEW LEAVES TAB
        SORT_DROPBOX.SelectedIndex = 0
        ORDINARY.Checked = True
        MEDICAL.Checked = True
        ACADEMIC.Checked = True
        ACCEPTED.Checked = True
        REJECTED.Checked = True
        PENDING.Checked = True
        CANCELLED.Checked = True
        ORDINARY_2.Checked = True
        MEDICAL_2.Checked = True
        ACADEMIC_2.Checked = True
        DROPBOX_2.SelectedIndex = 0
        NEWEST_2.Checked = True
        NEWEST_CHECKBOX.Checked = True

        RefreshViewLeaves()

    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Me.Close()
        Form1.Show()
    End Sub


    Private Sub tabpgViewLeaves_Enter(sender As Object, e As EventArgs) Handles tabpgViewLeaves.Enter
        RefreshViewLeaves()
    End Sub

    Private Sub tabpgLeavesToApprove_Enter(sender As Object, e As EventArgs) Handles tabpgLeavesToApprove.Enter
        RefreshLeavestoApprove()
    End Sub

    Private Sub RefreshLeavestoApprove()
        ' Clear existing listview
        APPROVED.Items.Clear()


        If NEWEST_2.Checked = True Then
            If DROPBOX_2.Text = "Date / Time Applied" Then
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY_2.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL_2.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC_2.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [Date/Time Applied] DESC")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            ElseIf DROPBOX_2.Text = "Start Date" Then

                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY_2.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL_2.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC_2.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [Start_Date] DESC")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            ElseIf DROPBOX_2.Text = "End Date" Then

                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY_2.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL_2.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC_2.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [End_Date] DESC")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            Else
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY_2.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL_2.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC_2.Checked = True Then
                    dummy3 = "Academic"
                End If

                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "')")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            End If


        Else
            If DROPBOX_2.Text = "Date / Time Applied" Then
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [Date/Time Applied]")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            ElseIf DROPBOX_2.Text = "Start Date" Then
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [Start_Date]")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            ElseIf DROPBOX_2.Text = "End Date" Then
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') ORDER BY [End_Date]")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            Else
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "')")
                Dim i As Integer = 0
                ' Get username of faculty
                Dim user As String = Label1.Text
                ' Populate listview
                If Access.RecordCount > 0 Then
                    ' Cycle through all leaves
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim s As String
                        Dim flag As Integer = 0
                        ' Get list of participating users for leave
                        s = Access.DBDT.Rows(i).Item("List_of_Participating_Users")
                        Dim s1 As String = ""

                        ' Extract usernames from list of participating users
                        For Each c As Char In s
                            If c <> "," And c <> " " Then
                                s1 = s1 + c
                            Else
                                If s1 = user Then
                                    ' Set flag = 1 if username matches username of logged in faculty
                                    flag = 1
                                    Exit For
                                End If
                                s1 = ""
                            End If
                        Next

                        If flag = 1 Then
                            ' Add leave to listview
                            Dim dum As String = r(6)
                            Dim v As New ListViewItem(dum)
                            v.SubItems.Add(r(0))
                            v.SubItems.Add(r(1))
                            v.SubItems.Add(r(4))
                            v.SubItems.Add(r(5))
                            v.SubItems.Add(r(3))
                            APPROVED.Items.Add(v)
                        End If
                        i = i + 1
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub tabpgNotifications_Enter(sender As Object, e As EventArgs) Handles tabpgNotifications.Enter
        'RefreshViewLeaves()
        Dim user As String = Label1.Text
        Dim i As Integer = 0
        Access.ExecQuery("SELECT * FROM Leave_Update_DB WHERE Username='" & user & "'")
        If Access.RecordCount > 0 Then
            For Each r As DataRow In Access.DBDT.Rows
                Dim dum As String = r(0)
                Dim v As New ListViewItem(dum)
                v.SubItems.Add(r(4))
                v.SubItems.Add(r(2))
                Dim dum1 As String = ""
                dum1 = dum1 + " Your Leave having Leave ID " + r(0) + " is " + r(5) + " by " + r(6)
                v.SubItems.Add(dum1)
                NOTIFICATIONS.Items.Add(v)
            Next

        Else
            MessageBox.Show("No Notications!!!!")
        End If

    End Sub



    Private Sub RefreshViewLeaves()
        ' CLEAR EXISTING DATA IN LISTVIEW
        lsviewViewLeavesListOfLeaves.Items.Clear()

        ' REPOPULATE LISTVIEW

        If NEWEST_CHECKBOX.Checked = True Then
            If SORT_DROPBOX.Text = "Date / Time Applied" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [Date/Time Applied] DESC")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            ElseIf SORT_DROPBOX.Text = "Start Date" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [Start_Date] DESC")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            ElseIf SORT_DROPBOX.Text = "End Date" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [End_Date] DESC")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            Else
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "')")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            End If

        Else
            If SORT_DROPBOX.Text = "Date / Time Applied" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [Date/Time Applied]")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            ElseIf SORT_DROPBOX.Text = "Start Date" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [Start_Date]")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            ElseIf SORT_DROPBOX.Text = "End Date" Then
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "') ORDER BY [End_Date]")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            Else
                'FOR STATUS
                Dim dum1 As String = ""
                Dim dum2 As String = ""
                Dim dum3 As String = ""
                Dim dum4 As String = ""
                If ACCEPTED.Checked = True Then
                    dum1 = "Accepted"
                End If
                If REJECTED.Checked = True Then
                    dum2 = "Rejected"
                End If
                If PENDING.Checked = True Then
                    dum3 = "Pending"
                End If
                If CANCELLED.Checked = True Then
                    dum4 = "Cancelled"
                End If
                'FOR TYPE
                Dim dummy1 As String = ""
                Dim dummy2 As String = ""
                Dim dummy3 As String = ""
                If ORDINARY.Checked = True Then
                    dummy1 = "Ordinary"
                End If
                If MEDICAL.Checked = True Then
                    dummy2 = "Medical"
                End If
                If ACADEMIC.Checked = True Then
                    dummy3 = "Academic"
                End If
                Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "' and Type_of_Leave IN ('" & dummy1 & "','" & dummy2 & "','" & dummy3 & "') and Current_Status IN ('" & dum1 & "','" & dum2 & "','" & dum3 & "','" & dum4 & "')")
                If Access.RecordCount > 0 Then
                    For Each r As DataRow In Access.DBDT.Rows
                        Dim dum As String = r(1)
                        Dim v As New ListViewItem(dum)
                        v.SubItems.Add(r(6))
                        v.SubItems.Add(r(3))
                        v.SubItems.Add(r(4))
                        v.SubItems.Add(r(5))
                        v.SubItems.Add(r(2))
                        lsviewViewLeavesListOfLeaves.Items.Add(v)
                    Next
                End If
            End If

        End If








        'Access.ExecQuery("SELECT * FROM Leave_DB WHERE Username='" & Label1.Text & "'")
        'If Access.RecordCount > 0 Then
        '    For Each r As DataRow In Access.DBDT.Rows
        '        Dim dum As String = r(1)
        '        Dim v As New ListViewItem(dum)
        '        v.SubItems.Add(r(6))
        '        v.SubItems.Add(r(3))
        '        v.SubItems.Add(r(4))
        '        v.SubItems.Add(r(5))
        '        v.SubItems.Add(r(2))
        '        lsviewViewLeavesListOfLeaves.Items.Add(v)
        '    Next
        'End If
        ' TODO

    End Sub

    Private Sub Date_Calc_DateChanged(sender As Object, e As DateRangeEventArgs) Handles Date_Calc.DateChanged
        Start_Date = e.Start
        End_Date = e.End
        E_Date.Enabled = False
        S_Date.Enabled = False
        S_Date.Text = e.Start.ToShortDateString()
        E_Date.Text = e.End.ToShortDateString()
    End Sub

    Private Sub Submit_new_Click(sender As Object, e As EventArgs) Handles Submit_new.Click
        'Getting all the Required Inputs fr database
        End_Date = CDate(End_Date.ToShortDateString)
        Dim D1 As Date = Start_Date
        Dim D2 As Date = End_Date
        'Calculating The duration of leave
        Dim difference As TimeSpan = D2.Subtract(D1)
        Dim number_of_days As Integer = difference.TotalDays
        Dim remark As String = Remark_Box.Text
        Dim type As String = Type_Of_Leave.Text
        Dim status As String = "Pending"
        Dim username As String = Label1.Text
        Dim d As Date = System.DateTime.Now()
        'Generating Leave_ID
        Dim l_id As String = ""
        l_id = username(0) + username(1) + username(0) + username(1)
        Dim d_2 As String = Nothing
        d_2 = d.ToString()
        For Each c As Char In d_2
            If c <> " " And c <> "-" And c <> ":" Then
                l_id = l_id + c
            End If
        Next
        'MessageBox.Show(l_id)
        Dim list_of_Update As String = l_id
        Dim list_of_participating_users As String = ""

        Dim s As String = "HOD"
        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Designation='" & s & "'")
        Dim hod As String = Access.DBDT.Rows(0).Item(0)

        Dim s2 As String = "ADOAA"
        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Designation='" & s2 & "'")
        Dim adoaa As String = Access.DBDT.Rows(0).Item("Username")

        Access.AddParam("@user", username)
        Access.ExecQuery("SELECT * FROM Student_DB WHERE Username=@user")



        'If the Student is logged in
        If Access.RecordCount > 0 Then
            'Adding TA Superviser and guide in list of participating users
            Dim ta As String = Access.DBDT.Rows(0).Item("TA_Superviser")
            Dim guide As String = Access.DBDT.Rows(0).Item("Guide")
            Dim BALANCE_LEFT As Integer = Access.DBDT.Rows(0).Item(type)
            If type = "Ordinary" Then
                If BALANCE_LEFT < number_of_days Then
                    MessageBox.Show("Can't apply for ordinary leave")
                    Exit Sub
                Else
                    Dim help As Integer = BALANCE_LEFT - number_of_days
                    Access.ExecQuery("UPDATE Student_DB SET Ordinary=" & help & " WHERE Username='" & username & "'")
                End If
            ElseIf type = "Medical" Then
                If BALANCE_LEFT < number_of_days Then
                    MessageBox.Show("Can't apply for medical leave")
                    Exit Sub
                Else
                    Dim help As Integer = BALANCE_LEFT - number_of_days
                    Access.ExecQuery("UPDATE Student_DB SET Medical=" & help & " WHERE Username='" & username & "'")
                End If
            Else
                If BALANCE_LEFT < number_of_days Then
                    MessageBox.Show("Can't apply for academic leave")
                    Exit Sub
                Else
                    Dim help As Integer = BALANCE_LEFT - number_of_days
                    Access.ExecQuery("UPDATE Student_DB SET Academic=" & help & " WHERE Username='" & username & "'")
                End If
            End If


            If ta <> "" Then
                list_of_participating_users = list_of_participating_users + ta + ","
                Dim help_3 As String = ta
                Access.AddParam("@help_3", help_3)
                Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                Dim help_4 As String = Nothing
                If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                    help_4 = l_id + ","
                Else
                    help_4 = Access.DBDT.Rows(0).Item(0)
                    help_4 = help_4 + l_id + ","
                End If
                Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
            End If

            If guide <> "" Then
                list_of_participating_users = list_of_participating_users + guide + ","
                Dim help_3 As String = guide
                Access.AddParam("@help_3", help_3)
                Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                Dim help_4 As String = Nothing
                If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                    help_4 = l_id + ","
                Else
                    help_4 = Access.DBDT.Rows(0).Item(0)
                    help_4 = help_4 + l_id + ","
                End If
                Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
            End If

            'Deciding the UPSTREAM of leave by checking leave duration and type of leave
            If Type_Of_Leave.Text = "Ordinary" Then
                If number_of_days > 15 Then
                    list_of_participating_users = list_of_participating_users + hod + ","
                    Dim help_3 As String = hod
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
                If number_of_days > 30 Then
                    list_of_participating_users = list_of_participating_users + adoaa + ","
                    Dim help_3 As String = adoaa
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
            End If
            If Type_Of_Leave.Text = "Medical" Then
                If number_of_days > 15 Then
                    list_of_participating_users = list_of_participating_users + hod + ","
                    Dim help_3 As String = hod
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
                If number_of_days > 30 Then
                    list_of_participating_users = list_of_participating_users + adoaa + ","
                    Dim help_3 As String = adoaa
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
            End If
            If Type_Of_Leave.Text = "Academic" Then
                If number_of_days > 15 Then
                    list_of_participating_users = list_of_participating_users + hod + ","
                    Dim help_3 As String = hod
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
                If number_of_days > 30 Then
                    list_of_participating_users = list_of_participating_users + adoaa + ","
                    Dim help_3 As String = adoaa
                    Access.AddParam("@help_3", help_3)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_4 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_4 = l_id + ","
                    Else
                        help_4 = Access.DBDT.Rows(0).Item(0)
                        help_4 = help_4 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")
                End If
            End If
            'Entering the Data into the Leave DataBase
            Access.AddParam("@user2", username)
            Access.AddParam("@date2", d)
            Access.AddParam("@current", status)
            Access.AddParam("@type2", type)
            Access.AddParam("@start_d", Start_Date)
            Access.AddParam("@end_d", End_Date)
            Access.AddParam("@leave_id", l_id)
            Access.AddParam("@lopu", list_of_participating_users)
            Access.AddParam("@leave_log", list_of_Update)
            Access.AddParam("@remarks", remark)
            'Insert Query to insert into the Leave DATABASE
            Access.ExecQuery("INSERT INTO Leave_DB([Username], [Date/Time Applied], [Current_Status], [Type_of_Leave], [Start_Date], [End_Date], [Leave_ID], [List_of_Participating_Users], [Leave_Log], [Remarks])VALUES(@user2, @date2, @current, @type2, @start_d, @end_d, @leave_id, @lopu, @leave_log, @remarks)")

            'Entering the data into the update DataBase
            Access.AddParam("@leave_id2", l_id)
            Access.AddParam("@leave_id3", l_id)
            Access.AddParam("@date3", d)
            Access.AddParam("@user3", username)
            Dim dum As String = ""
            Dim dum2 As String = "Pending"
            Access.AddParam("@dum", dum)
            Access.AddParam("@dum2", dum2)
            Access.AddParam("@user4", username)
            Access.ExecQuery("INSERT INTO Leave_Update_DB([Leave_ID], [Update_ID], [Date/Time], [Username], [Remark], [Updated_Status], [Username_Action])VALUES(@leave_id2, @leave_id3, @date3, @user3, @dum, @dum2, @user4)")
        Else
            'For the case when Faculty is Logged in
            Access.AddParam("@user2", username)
            Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Username = @user2")
            If Access.RecordCount > 0 Then
                'Adding HOD and ADOAA in list of participating Users
                list_of_participating_users = list_of_participating_users + hod + ","

                Dim help_3 As String = hod
                Access.AddParam("@help_3", help_3)
                Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                Dim help_4 As String = Nothing
                If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                    help_4 = l_id + ","
                Else
                    help_4 = Access.DBDT.Rows(0).Item(0)
                    help_4 = help_4 + l_id + ","
                End If
                Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_4 & "' WHERE Username='" & help_3 & "'")

                If number_of_days > 30 Then
                    list_of_participating_users = list_of_participating_users + adoaa + ","
                    Dim help_5 As String = hod
                    Access.AddParam("@help_3", help_5)
                    Access.ExecQuery("SELECT List_of_Incoming_Leaves FROM Faculty_DB WHERE Username=@help_3")
                    Dim help_6 As String = Nothing
                    If (IsDBNull(Access.DBDT.Rows(0).Item(0))) Then
                        help_6 = l_id + ","
                    Else
                        help_6 = Access.DBDT.Rows(0).Item(0)
                        help_6 = help_6 + l_id + ","
                    End If
                    Access.ExecQuery("UPDATE Faculty_DB SET List_of_Incoming_Leaves='" & help_6 & "' WHERE Username='" & help_5 & "'")
                End If

                Dim BALANCE_LEFT As Integer = Access.DBDT.Rows(0).Item(type)
                If type = "Ordinary" Then
                    If BALANCE_LEFT < number_of_days Then
                        MessageBox.Show("Can't apply for ordinary leave")
                        Exit Sub
                    Else
                        Dim help As Integer = BALANCE_LEFT - number_of_days
                        Access.ExecQuery("UPDATE Faculty_DB SET Ordinary=" & help & " WHERE Username ='" & username & "'")
                    End If
                ElseIf type = "Medical" Then
                    If BALANCE_LEFT < number_of_days Then
                        MessageBox.Show("Can't apply for medical leave")
                        Exit Sub
                    Else
                        Dim help As Integer = BALANCE_LEFT - number_of_days
                        Access.ExecQuery("UPDATE Faculty_DB SET Medical=" & help & " WHERE Username ='" & username & "'")
                    End If
                Else
                    If BALANCE_LEFT < number_of_days Then
                        MessageBox.Show("Can't apply for academic leave")
                        Exit Sub
                    Else
                        Dim help As Integer = BALANCE_LEFT - number_of_days
                        Access.ExecQuery("UPDATE Faculty_DB SET Academic=" & help & " WHERE Username ='" & username & "'")
                    End If
                End If

                'Adding Parameters for the insert query
                Access.AddParam("@user2", username)
                Access.AddParam("@date2", d)
                Access.AddParam("@current", status)
                Access.AddParam("@type2", type)
                Access.AddParam("@start_d", Start_Date)
                Access.AddParam("@end_d", End_Date)
                Access.AddParam("@leave_id", l_id)
                Access.AddParam("@lopu", list_of_participating_users)
                Access.AddParam("@leave_log", list_of_Update)
                Access.AddParam("@remarks", remark)
                'Insert Command for Entering the Data into the Leave Database
                Access.ExecQuery("INSERT INTO Leave_DB([Username], [Date/Time Applied], [Current_Status], [Type_of_Leave], [Start_Date], [End_Date], [Leave_ID], [List_of_Participating_Users], [Leave_Log], [Remarks])VALUES(@user2, @date2, @current, @type2, @start_d, @end_d, @leave_id, @lopu, @leave_log, @remarks)")

                'Adding parameters for the Insert query in Update DB
                Access.AddParam("@leave_id2", l_id)
                Access.AddParam("@leave_id3", l_id)
                Access.AddParam("@date3", d)
                Access.AddParam("@user3", username)
                Dim dum As String = ""
                Dim dum2 As String = "Pending"
                Access.AddParam("@dum", dum)
                Access.AddParam("@dum2", dum2)
                Access.AddParam("@user4", username)
                'Insert Command for the Update_DB
                Access.ExecQuery("INSERT INTO Leave_Update_DB([Leave_ID], [Update_ID], [Date/Time], [Username], [Remark], [Updated_Status], [Username_Action])VALUES(@leave_id2, @leave_id3, @date3, @user3, @dum, @dum2, @user4)")

            End If

        End If
        MessageBox.Show("LEAVE APLLIED SUCCESSFULLY")
    End Sub

    'This Button is for EDITING the profile
    Private Sub Edit_Click(sender As Object, e As EventArgs) Handles Edit.Click

        'Not making username Editable
        Form2.USERNAME.Text = Label1.Text
        Form2.USERNAME.Enabled = False
        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Username='" & Label1.Text & "'")
        'if the faculty is logged in (Includes the case of HOD
        If Access.RecordCount > 0 Then

            Form2.FIRST_NAME.Text = Access.DBDT.Rows(0).Item("First_Name")
            Form2.LAST_NAME.Text = Access.DBDT.Rows(0).Item("Last_Name")
            'FOR Faculties which are not HOD
            If Access.DBDT.Rows(0).Item("Designation") <> "HOD" Then
                Form2.Faculty_Checkbox.Checked = True
                Form2.Student_Checkbox.Enabled = False
                Form2.HOD_CheckBox.Enabled = False
                Form2.DEPARTMENT_FAC.Text = Access.DBDT.Rows(0).Item("Department")
                Form2.DESIGNATION.Text = Access.DBDT.Rows(0).Item("Designation")
                Form2.PASSWORD.Text = Access.DBDT.Rows(0).Item("Password")
                Form2.PASSWORD.Enabled = False
                Form2.PASSWORD.PasswordChar = "*"
                Form2.DEPARTMENT_FAC.Visible = True
                Form2.DESIGNATION.Visible = True

                'For The HOD
            Else
                Form2.DEPARTMENT_FAC.Text = Access.DBDT.Rows(0).Item("Department")
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
            Access.AddParam("@user2", Label1.Text)
            Access.ExecQuery("SELECT * FROM Student_DB WHERE Username=@user2")
            If Access.RecordCount > 0 Then
                'Getting all the old Deatails and filling into the EDIT FORM
                Form2.FIRST_NAME.Text = Access.DBDT.Rows(0).Item("First_name")
                Form2.LAST_NAME.Text = Access.DBDT.Rows(0).Item("Last_name")
                Form2.ROLL_NO.Text = Access.DBDT.Rows(0).Item("Roll_no")
                Form2.YEAR.Text = Access.DBDT.Rows(0).Item("Year_of_joining")
                Form2.PROGRAMME.Text = Access.DBDT.Rows(0).Item("Programme")
                Form2.TA_SUPERVISER.Text = Access.DBDT.Rows(0).Item("TA_Superviser")
                Form2.GUIDE.Text = Access.DBDT.Rows(0).Item("Guide")
                Form2.DEPARTMENT.Text = Access.DBDT.Rows(0).Item("Department")
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

    'FOR LEAVES TO BE APPROVED
    Private Function APPROVED_SelectedItem() As ListViewItem
        Dim selectedLeave As New ListViewItem
        If APPROVED.SelectedItems.Count > 0 Then
            selectedLeave = APPROVED.SelectedItems(0)
        End If

        Return selectedLeave
    End Function

    Private Sub btnLeavestobeApprovedView_Click(sender As Object, e As EventArgs) Handles btnLeavestobeApprovedView.Click
        Dim selectedLeave As New ListViewItem
        selectedLeave = APPROVED_SelectedItem()
        If selectedLeave.SubItems(0).Text() = "" Then
            MsgBox("No Leave selected!")
            Exit Sub
        End If

        Access.AddParam("@LID", selectedLeave.SubItems(0).Text())
        Access.ExecQuery("SELECT Current_Status FROM Leave_DB WHERE Leave_ID=@LID")

        Form5.txtDateTime.Text() = selectedLeave.SubItems(2).Text()
        Form5.txtLeaveID.Text() = selectedLeave.SubItems(0).Text()
        Form5.txtTypeofLeave.Text() = selectedLeave.SubItems(5).Text()
        Form5.txtStartDate.Text() = selectedLeave.SubItems(3).Text()
        Form5.txtEndDate.Text() = selectedLeave.SubItems(4).Text()
        Form5.txtCurrentStatus.Text() = Access.DBDT.Rows(0).Item(0)
        Form5.Show()

    End Sub

    Private Sub VIEW_Click(sender As Object, e As EventArgs) Handles VIEW.Click
        'take that from selected list view rows
        Dim dum As New ListViewItem
        dum = ADMIN_SelectedItem()
        If dum.SubItems(0).Text() = "" Then
            MsgBox("No entry selected!")
            Exit Sub
        End If

        Access.ExecQuery("SELECT * FROM Student_DB WHERE Username='" & dum.SubItems(0).Text() & "'")
        If Access.RecordCount > 0 Then
            'Getting all the old Deatails and filling into the EDIT FORM
            Form4.FULLNAME_TB.Text = Access.DBDT.Rows(0).Item("Username")
            Form4.USERNAME_TB.Text = Access.DBDT.Rows(0).Item("First_name")
            Form4.LAST_NAME_TB.Text = Access.DBDT.Rows(0).Item("Last_name")
            Form4.OPT_TB.Text = Access.DBDT.Rows(0).Item("Roll_no")
            Form4.YEAR_OF_JOINING_TB.Text = Access.DBDT.Rows(0).Item("Year_of_joining")
            Form4.PROGRAMME_TB.Text = Access.DBDT.Rows(0).Item("Programme")
            Form4.TA_SUPERVISER_TB.Text = Access.DBDT.Rows(0).Item("TA_Superviser")
            Form4.GUIDE_TB.Text = Access.DBDT.Rows(0).Item("Guide")
            Form4.DEPARTMENT_TB.Text = Access.DBDT.Rows(0).Item("Department")
            Form4.DESIGNATION.Visible = False
        End If

        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Username='" & dum.SubItems(0).Text() & "'")
        If Access.RecordCount > 0 Then
            'Getting all the old Deatails and filling into the EDIT FORM
            Form4.FULLNAME_TB.Text = Access.DBDT.Rows(0).Item("Username")
            Form4.USERNAME_TB.Text = Access.DBDT.Rows(0).Item("First_Name")
            Form4.LAST_NAME_TB.Text = Access.DBDT.Rows(0).Item("Last_Name")
            Form4.DEPARTMENT_TB.Text = Access.DBDT.Rows(0).Item("Department")
            Form4.OPT_TB.Text = Access.DBDT.Rows(0).Item("Designation")
            Form4.ROLL_NO.Visible = False
            Form4.YEAR_OF_JOINING.Visible = False
            Form4.YEAR_OF_JOINING_TB.Visible = False
            Form4.PROGRAMME_TB.Visible = False
            Form4.PROGRAMME.Visible = False
            Form4.TA_SUPERVISER.Visible = False
            Form4.TA_SUPERVISER_TB.Visible = False
            Form4.GUIDE.Visible = False
            Form4.GUIDE.Visible = False

        End If
        Me.Hide()
        Form4.Show()
    End Sub

    'FOR ADMIN
    Private Sub APPROVE_Click(sender As Object, e As EventArgs) Handles APPROVE.Click
        Dim dum As New ListViewItem
        dum = ADMIN_SelectedItem()
        If dum.SubItems(0).Text() = "" Then
            MsgBox("No entry selected!")
            Exit Sub
        End If

        Dim username As String = dum.SubItems(0).Text()
        Dim help As String = "YES"
        Dim dum2 As New ListViewItem
        dum2 = ADMIN_SelectedItem()
        If dum2.SubItems(4).Text() = "" Then
            MsgBox("No entry selected!")
            Exit Sub
        End If
        Dim type As String = dum.SubItems(4).Text()
        If type = "FACULTY" Then
            Access.ExecQuery("UPDATE Faculty_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
        Else
            Access.ExecQuery("UPDATE Student_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
        End If
        Access.ExecQuery("UPDATE Faculty_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
    End Sub

    'FOR LEAVES TO BE APPROVED
    Private Sub btnLeavestobeApprovedAccept_Click(sender As Object, e As EventArgs) Handles btnLeavestobeApprovedAccept.Click
        AcceptReject(True)
    End Sub

    'FOR LEAVES TO BE APPROVED
    Private Sub btnLeavestobeApprovedReject_Click(sender As Object, e As EventArgs) Handles btnLeavestobeApprovedReject.Click
        AcceptReject(False)
    End Sub

    'FOR LEAVES TO BE APPROVED
    Private Sub AcceptReject(isAccepted As Boolean)
        Dim selectedLeave As New ListViewItem
        selectedLeave = APPROVED_SelectedItem()
        If selectedLeave.SubItems(0).Text() = "" Then
            MsgBox("No leave selected!")
            Exit Sub
        End If


        Dim leave_ID As String = selectedLeave.SubItems(0).Text()
        Dim update_ID As String = ""



        Dim date_Time As Date = System.DateTime.Now()
        Dim user As String = selectedLeave.SubItems(1).Text()
        Dim remark As String = richtxtboxLeavestobeApprovedRemarks.Text()

        Dim status As String = ""
        If isAccepted Then
            status = "Accepted"
        Else
            status = "Rejected"
        End If

        Dim user_action As String = Label1.Text()

        'Generating Update_ID
        update_ID = user(0) + user(1) + user_action(0) + user_action(1)
        Dim d As String = date_Time.ToString()
        For Each c As Char In d
            If c <> " " And c <> "-" And c <> ":" Then
                update_ID = update_ID + c
            End If
        Next

        'Adding parameters for the Insert query in Update DB
        Access.AddParam("@LID", leave_ID)
        Access.AddParam("@UID", update_ID)
        Access.AddParam("@date", date_Time)
        Access.AddParam("@user", user)
        Access.AddParam("@remark", remark)
        Access.AddParam("@status", status)
        Access.AddParam("@user_act", user_action)
        'Insert Command for the Update_DB
        Access.ExecQuery("INSERT INTO Leave_Update_DB([Leave_ID], [Update_ID], [Date/Time], [Username], [Remark], [Updated_Status], [Username_Action])VALUES(@LID, @UID, @date, @user, @remark, @status, @user_act)")
        MsgBox("Leave status updated.")
        richtxtboxLeavestobeApprovedRemarks.Clear()

    End Sub

    Private Sub ACCEPTED_Click(sender As Object, e As EventArgs) Handles ACCEPTED.Click
        RefreshViewLeaves()
    End Sub

    Private Sub MEDICAL_Click(sender As Object, e As EventArgs) Handles MEDICAL.Click
        RefreshViewLeaves()
    End Sub

    Private Sub PENDING_Click(sender As Object, e As EventArgs) Handles PENDING.Click
        RefreshViewLeaves()
    End Sub

    Private Sub REJECTED_Click(sender As Object, e As EventArgs) Handles REJECTED.Click
        RefreshViewLeaves()
    End Sub

    Private Sub ACADEMIC_Click(sender As Object, e As EventArgs) Handles ACADEMIC.Click
        RefreshViewLeaves()
    End Sub

    Private Sub SORT_DROPBOX_Click(sender As Object, e As EventArgs) Handles SORT_DROPBOX.Click
        RefreshViewLeaves()
    End Sub

    Private Sub CANCELLED_Click(sender As Object, e As EventArgs) Handles CANCELLED.Click
        RefreshViewLeaves()
    End Sub

    Private Sub ORDINARY_Click(sender As Object, e As EventArgs) Handles ORDINARY.Click
        RefreshViewLeaves()
    End Sub

    Private Sub NEWEST_CHECKBOX_Click(sender As Object, e As EventArgs) Handles NEWEST_CHECKBOX.Click
        RefreshViewLeaves()
    End Sub

    Private Sub OLD_CHECKBOX_Click(sender As Object, e As EventArgs) Handles OLD_CHECKBOX.Click
        RefreshViewLeaves()
    End Sub


    Private Sub btnViewLeavesView_Click(sender As Object, e As EventArgs) Handles btnViewLeavesView.Click
        Dim selectedLeave As New ListViewItem
        selectedLeave = viewleaves_SelectedItem()

        Form5.txtDateTime.Text() = selectedLeave.SubItems(0).Text()
        Form5.txtLeaveID.Text() = selectedLeave.SubItems(1).Text()
        Form5.txtTypeofLeave.Text() = selectedLeave.SubItems(2).Text()
        Form5.txtStartDate.Text() = selectedLeave.SubItems(3).Text()
        Form5.txtEndDate.Text() = selectedLeave.SubItems(4).Text()
        Form5.txtCurrentStatus.Text() = selectedLeave.SubItems(5).Text()
        Form5.Show()
    End Sub

    Private Function viewleaves_SelectedItem() As ListViewItem
        Dim selectedLeave As New ListViewItem
        If lsviewViewLeavesListOfLeaves.SelectedItems.Count > 0 Then
            selectedLeave = lsviewViewLeavesListOfLeaves.SelectedItems(0)
        End If

        Return selectedLeave
    End Function

    Private Sub btnViewLeavesCancel_Click(sender As Object, e As EventArgs) Handles btnViewLeavesCancel.Click
        Dim selectedLeave As New ListViewItem
        selectedLeave = viewleaves_SelectedItem()
        If selectedLeave.SubItems(1).Text() = "" Then
            MsgBox("No leave selected!")
            Exit Sub
        End If

        Dim leave_ID As String = selectedLeave.SubItems(0).Text()
        Dim update_ID As String = ""

        Dim date_Time As Date = System.DateTime.Now()
        Dim user As String = Label1.Text()
        Dim remark As String = richtxtboxViewLeaves.Text()

        Dim status As String = "Cancelled"

        Dim user_action As String = user

        'Generating Update_ID
        update_ID = user(0) + user(1) + user_action(0) + user_action(1)
        Dim d As String = date_Time.ToString()
        For Each c As Char In d
            If c <> " " And c <> "-" And c <> ":" Then
                update_ID = update_ID + c
            End If
        Next

        'Adding parameters for the Insert query in Update DB
        Access.AddParam("@LID", leave_ID)
        Access.AddParam("@UID", update_ID)
        Access.AddParam("@date", date_Time)
        Access.AddParam("@user", user)
        Access.AddParam("@remark", remark)
        Access.AddParam("@status", status)
        Access.AddParam("@user_act", user_action)
        'Insert Command for the Update_DB
        Access.ExecQuery("INSERT INTO Leave_Update_DB([Leave_ID], [Update_ID], [Date/Time], [Username], [Remark], [Updated_Status], [Username_Action])VALUES(@LID, @UID, @date, @user, @remark, @status, @user_act)")
        MsgBox("Leave status updated.")
        richtxtboxViewLeaves.Clear()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'take that from selected list view rows
        Dim dum As String = Label1.Text()
        Access.ExecQuery("SELECT * FROM Student_DB WHERE Username='" & dum & "'")
        If Access.RecordCount > 0 Then
            'Getting all the old Deatails and filling into the EDIT FORM
            Form4.USERNAME_TB.Text = Access.DBDT.Rows(0).Item("Username")
            Form4.FIRST_NAME_TB.Text = Access.DBDT.Rows(0).Item("First_name")
            Form4.LAST_NAME_TB.Text = Access.DBDT.Rows(0).Item("Last_name")
            Form4.OPT_TB.Text = Access.DBDT.Rows(0).Item("Roll_no")
            Form4.YEAR_OF_JOINING_TB.Text = Access.DBDT.Rows(0).Item("Year_of_joining")
            Form4.PROGRAMME_TB.Text = Access.DBDT.Rows(0).Item("Programme")
            Form4.TA_SUPERVISER_TB.Text = Access.DBDT.Rows(0).Item("TA_Superviser")
            Form4.GUIDE_TB.Text = Access.DBDT.Rows(0).Item("Guide")
            Form4.DEPARTMENT_TB.Text = Access.DBDT.Rows(0).Item("Department")
            Form4.DESIGNATION.Visible = False
        End If

        Access.ExecQuery("SELECT * FROM Faculty_DB WHERE Username='" & dum & "'")
        If Access.RecordCount > 0 Then
            'Getting all the old Deatails and filling into the EDIT FORM
            Form4.USERNAME_TB.Text = Access.DBDT.Rows(0).Item("Username")
            Form4.FIRST_NAME_TB.Text = Access.DBDT.Rows(0).Item("First_Name")
            Form4.LAST_NAME_TB.Text = Access.DBDT.Rows(0).Item("Last_Name")
            Form4.DEPARTMENT_TB.Text = Access.DBDT.Rows(0).Item("Department")
            Form4.OPT_TB.Text = Access.DBDT.Rows(0).Item("Designation")
            Form4.ROLL_NO.Visible = False
            Form4.YEAR_OF_JOINING.Visible = False
            Form4.YEAR_OF_JOINING_TB.Visible = False
            Form4.PROGRAMME_TB.Visible = False
            Form4.PROGRAMME.Visible = False
            Form4.TA_SUPERVISER.Visible = False
            Form4.TA_SUPERVISER_TB.Visible = False
            Form4.GUIDE.Visible = False
            Form4.GUIDE.Visible = False

        End If
        Me.Hide()
        Form4.Show()
    End Sub

    Private Sub ACADEMIC_2_Click(sender As Object, e As EventArgs) Handles ACADEMIC_2.Click
        RefreshLeavestoApprove()
    End Sub

    Private Sub MEDICAL_2_Click(sender As Object, e As EventArgs) Handles MEDICAL_2.Click
        RefreshLeavestoApprove()
    End Sub

    Private Sub ORDINARY_2_Click(sender As Object, e As EventArgs) Handles ORDINARY_2.Click
        RefreshLeavestoApprove()
    End Sub

    Private Sub DROPBOX_2_Click(sender As Object, e As EventArgs) Handles DROPBOX_2.Click
        RefreshLeavestoApprove()
    End Sub

    Private Sub NEWEST_2_Click(sender As Object, e As EventArgs) Handles NEWEST_2.Click
        RefreshLeavestoApprove()
    End Sub

    Private Sub OLDEST_2_Click(sender As Object, e As EventArgs) Handles OLDEST_2.Click
        RefreshLeavestoApprove()
    End Sub
    'FOR ADMIN

    Private Function ADMIN_SelectedItem() As ListViewItem
        Dim selectedEntry As New ListViewItem
        If ADMIN.SelectedItems.Count > 0 Then
            selectedEntry = ADMIN.SelectedItems(0)
        End If

        Return selectedEntry
    End Function

    Private Sub DISAPPROVE_Click(sender As Object, e As EventArgs) Handles DISAPPROVE.Click
        Dim dum As New ListViewItem
        dum = ADMIN_SelectedItem()
        If dum.SubItems(0).Text() = "" Then
            MsgBox("No entry selected!")
            Exit Sub
        End If

        Dim username As String = dum.SubItems(0).Text()
        Dim help As String = "NO"
        Dim dum2 As New ListViewItem
        dum2 = ADMIN_SelectedItem()
        If dum2.SubItems(4).Text() = "" Then
            MsgBox("No entry selected!")
            Exit Sub
        End If
        Dim type As String = dum.SubItems(4).Text()
        If type = "FACULTY" Then
            Access.ExecQuery("UPDATE Faculty_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
        Else
            Access.ExecQuery("UPDATE Student_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
        End If
        Access.ExecQuery("UPDATE Faculty_DB SET Approved=" & help & " WHERE Username ='" & username & "'")
    End Sub
End Class