namespace Assets.Scripts.CollisionHandlers
{
    public class NoteCollisionHandler : CollisionHandler {

        public SoundBite SoundBite;

        public override void OnCollision()
        {
            SoundBite.Play();

            base.OnCollision();
        }

    }
}
