extends KinematicBody2D

var is_moving_right = true

var gravity = 9.8
var velocity = Vector2(0, 0)

export var speed = 64

func _ready():
	$AnimatedSprite.play("run")

func _process(delta):
	attack_frame()
	
	move_character()
	detect_turn_around()
	
func move_character():
	velocity.x = speed if is_moving_right else -speed
	velocity.y += gravity
	print(velocity.x)
	
	velocity = move_and_slide(velocity, Vector2.UP)
	
func detect_turn_around():
	if not $RayCast2D.is_colliding() and is_on_floor():
		is_moving_right = !is_moving_right
		scale.x = -scale.x
		
func hit():
	$AttackDetector.monitoring = true
	
func end_of_hit():
	$AttackDetector.monitoring = false
	
func start_walk(): 
	$AnimatedSprite.play("run")
	
func attack_frame():
	if $AnimatedSprite.animation == "Atack1":
		var number_frame = $AnimatedSprite.frame
		if number_frame in [0, 1]:
			return
		match number_frame:
			2: hit()
			3:
				end_of_hit()
				start_walk()

func _on_PlayerDetector_body_entered(body):
	$AnimatedSprite.play("Atack1")

func _on_AttackDetector_body_entered(body):
	get_tree().reload_current_scene()


func _on_PlayerDetector2_body_entered(body):
	is_moving_right = !is_moving_right
	scale.x = -scale.x
	$AnimatedSprite.play("Atack1")
