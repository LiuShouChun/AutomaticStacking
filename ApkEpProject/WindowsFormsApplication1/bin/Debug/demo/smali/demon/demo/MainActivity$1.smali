.class Ldemon/demo/MainActivity$1;
.super Ljava/lang/Object;
.source "MainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ldemon/demo/MainActivity;->onCreate(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ldemon/demo/MainActivity;


# direct methods
.method constructor <init>(Ldemon/demo/MainActivity;)V
    .locals 0
    .param p1, "this$0"    # Ldemon/demo/MainActivity;

    .prologue
    .line 24
    iput-object p1, p0, Ldemon/demo/MainActivity$1;->this$0:Ldemon/demo/MainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0
    .param p1, "view"    # Landroid/view/View;

    .prologue
    .line 28
    return-void
.end method
