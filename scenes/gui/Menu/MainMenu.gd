extends Control

func _ready():
	pass



func _on_QuitButton_pressed():
	get_tree().quit()


func _on_AboutButton_pressed():
	get_tree().change_scene("res://scenes/gui/Menu/CreditsMenu.tscn")
