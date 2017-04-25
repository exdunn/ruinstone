
public static class Types {
	public enum Area {
        DEFAULT,
        DIRECT,
        LINEAR,
        POINT,
        CENTER
    }

    public enum Ability {
        DEFAULT,
        DAMAGE,
        HEAL,
        DISPLACE,
        DISRUPT,
        MISC
    }

    public enum Target {
        DEFAULT,
        ENEMY,
        SELF
    }

    public static string TargetToString(Target target) {
        switch(target) {
            case Target.DEFAULT:
                return "default";
            case Target.ENEMY:
                return "Player";
            case Target.SELF:
                return "self";
            default:
                return "";
        }
    }

}
