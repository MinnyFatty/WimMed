# WimMed Admin UI Living Documentation

This document captures key UI scenarios, error handling, and validation for the WimMed Admin application. It will be updated as new screenshots and scenarios are added.


## 6. Patient Deletion: Confirmation and Success

**Delete Confirmation Dialog:**
![Delete Confirmation](attachments/0.png)

**Patient Record Deleted Successfully:**
![Delete Success](attachments/1.png)

**Details:**
- When the user clicks Delete, a confirmation dialog appears to prevent accidental deletion.
- After confirming, the patient record and related info are deleted via the API.
- The patient list refreshes automatically.
- A success message is shown at the top of the page.

## 7. Patient List, Info Modal, and Sidebar Cleanup

**Sidebar with Version Label Removed:**
![Sidebar No Version](attachments/0.png)

**Patient List and Info Block:**
![Patient List](attachments/1.png)
![Patient Info Block](attachments/2.png)

**Edit Patient Info Modal:**
![Edit Patient Info Modal](attachments/3.png)

**Patient Info Updated Successfully:**
![Patient Info Updated](attachments/4.png)

**Details:**
- The sidebar no longer displays the version label ("v6.2.0"), as requested.
- The patient list allows searching, editing, and deleting patients.
- Clicking "Show Info" reveals a filtered info block (Age, Height, Weight, BMI, BMI Status).
- The "Edit Info" button opens a modal for editing only Age, Height, and Weight.
- After saving, a success message is shown and the info block refreshes.


**Screenshot:**
![Navigation Screenshot](attachments/5.png)

- The sidebar shows only the Dashboard for a clean, focused navigation experience.
- The top bar is minimal, with unnecessary items removed.

---

## 2. Error Handling: Failed to Update Patient

**Example 1:**
![Failed to update patient - DOB mismatch](attachments/0.png)

- When the date of birth does not match the ID number, the API returns a clear error message, which is displayed to the user.

**Example 2:**
![Failed to update patient - Invalid ID](attachments/1.png)

- Invalid South African ID number format is caught and shown in the alert.

---

## 3. Edit Patient Modal

**Screenshot:**
![Edit Patient Modal](attachments/2.png)

- The edit modal allows updating patient details with validation for required fields.

---

## 4. Search and Highlight

**Screenshot:**
![Search and Highlight](attachments/3.png)

- The search bar filters patients by name, ID, or other fields.

**Example:**
![Search Highlight Example](attachments/4.png)

- Matching text is highlighted in the results for clarity.

---

## 5. Validation Scenarios

- The form validates ID number and date of birth before submitting.
- API-side validation errors are shown in alerts for user correction.

---


