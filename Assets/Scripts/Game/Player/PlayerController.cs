using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 1000f;

    private float horizontalInput;

    private Rigidbody2D rb;

    public PlayerDataAsset PlayerDataAsset;


    [Networked] private NetworkButtons buttons { get; set; }


    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void BeforeUpdate()
    {
        // Local Player
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            PlayerDataAsset.UpdateInput(horizontalInput, InputButtons.Jump);

            CheckInput(PlayerDataAsset.Data);
        }
    }

    private void CheckInput(PlayerData input)
    {
        var buttonsInput = input.NetworkButtons.GetPressed(buttons);

        if (buttonsInput.WasPressed(buttons, InputButtons.Jump))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

        buttons = input.NetworkButtons;
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out var input))
        {
            rb.velocity = new Vector2(input.HorizontalInput * moveSpeed, rb.velocity.y);
        }
    }
}